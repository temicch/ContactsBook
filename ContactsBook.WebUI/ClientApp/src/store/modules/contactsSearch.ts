import {
  Module,
  getModule,
  VuexModule,
  Action,
  Mutation,
} from "vuex-module-decorators";

import store from "@/store";
import ContactsApiProvider from "@/api";

const defaultPageSize = 10;

@Module({ dynamic: true, store, namespaced: true, name: "search" })
export class ContactsSearch extends VuexModule implements ContactsSearchState {
  private apiProvider: ApiProvider<Contact> = new ContactsApiProvider();
  loading = false;
  searchPhrase: string = "";
  items: Contact[] = [];
  paginatedParams: PaginatedParams = {
    pageIndex: 0,
    pageSize: defaultPageSize,
    totalCount: 0,
    totalPages: 0,
    hasPreviousPage: false,
    hasNextPage: false,
  };

  @Mutation
  private CONTACTS_SET(payload: { contacts: Contact[] }) {
    this.items = payload.contacts;
  }

  @Mutation
  private CONTACTS_PUSH(payload: { contacts: Contact[] | Contact }) {
    if (Array.isArray(payload.contacts)) this.items.push(...payload.contacts);
    else this.items.push({ ...payload.contacts });
  }

  @Mutation
  private CONTACTS_REMOVE_AT(payload: { position: number }) {
    this.items.splice(payload.position, 1);
  }

  @Mutation
  private SEARCH_PHRASE_SET(payload: { searchPhrase: string }) {
    this.searchPhrase = payload.searchPhrase;
  }

  @Mutation
  private PAGINATED_PARAMS_SET(payload: Partial<PaginatedParams>) {
    this.paginatedParams = {
      hasPreviousPage:
        payload.hasPreviousPage ?? this.paginatedParams.hasPreviousPage,
      hasNextPage: payload.hasNextPage ?? this.paginatedParams.hasNextPage,
      pageIndex: payload.pageIndex ?? this.paginatedParams.pageIndex,
      pageSize: payload.pageSize ?? this.paginatedParams.pageSize,
      totalPages: payload.totalPages ?? this.paginatedParams.totalPages,
      totalCount: payload.totalCount ?? this.paginatedParams.totalCount,
    };
  }

  @Mutation
  private LOADING_STATE_SET(payload: { isLoading: boolean }) {
    this.loading = payload.isLoading;
  }

  @Action
  public async RemoveContact(contactId: string): Promise<void> {
    if (this.searchPhrase.length < 3) return;

    const position = this.items.map((x) => x.id).indexOf(contactId);

    if (position == -1) return;

    this.CONTACTS_REMOVE_AT({ position });
    const { pageIndex, pageSize } = this.paginatedParams;

    if (this.paginatedParams.hasNextPage == true) {
      await this.fetchContacts({
        pageSize: 1,
        isPush: true,
        pageIndex:
          this.paginatedParams.pageSize * (this.paginatedParams.pageIndex + 1) -
          1,
      });
    }

    await this.reloadPaginatedParams({ pageIndex, pageSize });
    if (this.paginatedParams.pageIndex >= this.paginatedParams.totalPages)
      this.PAGINATED_PARAMS_SET({
        pageIndex: this.paginatedParams.totalPages - 1,
      });
  }

  @Action({ rawError: true })
  public async CreateContact(contact: Contact): Promise<void> {
    this.ReloadContacts(this.searchPhrase);
  }

  @Action({ rawError: true })
  public async UpdateContact(contact: Contact): Promise<void> {
    this.ReloadContacts(this.searchPhrase);
  }

  @Action
  public async ReloadContacts(searchPhrase: string): Promise<void> {
    if (searchPhrase.length < 3) return;

    this.LOADING_STATE_SET({ isLoading: true });
    this.SEARCH_PHRASE_SET({ searchPhrase });
    await this.fetchContacts({ pageIndex: 0, isPush: false }).finally(() => {
      this.LOADING_STATE_SET({ isLoading: false });
    });
  }

  @Action
  public async NextPage(): Promise<boolean> {
    if (!this.paginatedParams.hasNextPage) return false;

    await this.fetchContacts({
      pageIndex: this.paginatedParams.pageIndex + 1,
      isPush: true,
    });

    return true;
  }

  @Action
  private async reloadPaginatedParams(payload: {
    pageIndex: number;
    pageSize: number;
  }) {
    return await this.apiProvider
      .GetAll(payload.pageIndex, payload.pageSize)
      .then((response) => {
        this.PAGINATED_PARAMS_SET(response);
      });
  }

  @Action
  private async fetchContacts(payload: {
    pageIndex: number;
    pageSize?: number;
    isPush: boolean;
  }): Promise<void> {
    return await this.apiProvider
      .GetAll(
        payload.pageIndex,
        payload.pageSize || this.paginatedParams.pageSize,
        this.searchPhrase,
        this.searchPhrase
      )
      .then((response) => {
        if (!payload.isPush) {
          this.CONTACTS_SET({ contacts: response.items });
          this.PAGINATED_PARAMS_SET(response);
        } else {
          this.CONTACTS_PUSH({ contacts: response.items });
          this.PAGINATED_PARAMS_SET(response);
        }
      });
  }
}

const ContactsSearchModule = getModule(ContactsSearch);

export default ContactsSearchModule;
