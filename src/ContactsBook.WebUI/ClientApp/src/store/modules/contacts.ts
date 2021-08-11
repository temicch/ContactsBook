import {
  VuexModule,
  Module,
  Action,
  Mutation,
  getModule
} from "vuex-module-decorators";

import store from "@/store";
import { binarySearch } from "@/utils";
import ContactsApiProvider from "@/api";

const defaultPageSize = 10;

@Module({ dynamic: true, store, name: "contacts" })
export class Contacts extends VuexModule implements ContactsState {
  loading = false;
  items: Contact[] = [];
  protected apiProvider: ApiProvider<Contact> = new ContactsApiProvider();
  paginatedParams: PaginatedParams = {
    pageIndex: 0,
    pageSize: defaultPageSize,
    totalCount: 0,
    totalPages: 0,
    hasPreviousPage: false,
    hasNextPage: false,
    isPageExists: true
  };

  @Mutation
  private CONTACTS_SET(payload: { contacts: Contact[] }) {
    this.items = payload.contacts;
  }

  @Mutation
  private CONTACT_SET(payload: { position: number; contact: Contact }) {
    this.items[payload.position] = payload.contact;
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
  private CONTACTS_INSERT_AT(payload: { contact: Contact; position: number }) {
    this.items.splice(payload.position, 0, payload.contact);
  }

  @Mutation
  private CONTACTS_POP() {
    this.items.pop();
  }

  @Mutation
  private PAGINATED_PARAMS_SET(payload: Partial<PaginatedParams>) {
    this.paginatedParams = {
      hasPreviousPage:
        payload.hasPreviousPage ?? this.paginatedParams.hasPreviousPage,
      hasNextPage: payload.hasNextPage ?? this.paginatedParams.hasNextPage,
      isPageExists: payload.isPageExists ?? this.paginatedParams.isPageExists,
      pageIndex: payload.pageIndex ?? this.paginatedParams.pageIndex,
      pageSize: payload.pageSize ?? this.paginatedParams.pageSize,
      totalPages: payload.totalPages ?? this.paginatedParams.totalPages,
      totalCount: payload.totalCount ?? this.paginatedParams.totalCount
    };
  }

  @Mutation
  private LOADING_STATE_SET(payload: { isLoading: boolean }) {
    this.loading = payload.isLoading;
  }

  @Action
  public async LoadContacts(): Promise<void> {
    this.LOADING_STATE_SET({ isLoading: true });
    return await this.fetchContacts({
      pageIndex: this.paginatedParams.pageIndex,
      isPush: false
    }).finally(() => {
      this.LOADING_STATE_SET({ isLoading: false });
    });
  }

  @Action({ rawError: true })
  public async CreateContact(contact: Contact): Promise<string> {
    const hasNextPage = this.paginatedParams.hasNextPage;

    const response = await this.apiProvider.Create<CreateContactResponse>(
      contact
    );

    contact.id = response.id;

    if ((await this.isCanBeInsertInMiddle({ contact })) == true) {
      let positionToInsert = binarySearch(
        this.items,
        contact,
        (a: Contact, b: Contact) => a.name.localeCompare(b.name)
      ).position;

      this.CONTACTS_INSERT_AT({ contact, position: positionToInsert });

      if ((await this.isItemsOverflow()) == true) {
        this.CONTACTS_POP();
      }
    }

    await this.reloadPaginatedParams({
      pageIndex: this.paginatedParams.pageIndex,
      pageSize: this.paginatedParams.pageSize
    });

    if (hasNextPage == false && this.paginatedParams.hasNextPage == true)
      this.NextPage();

    return response.id;
  }

  @Action({ rawError: true })
  public async UpdateContact(contact: Contact): Promise<void> {
    await this.apiProvider.Update(contact);

    const position = this.items.findIndex(
      (item: Contact) => item.id == contact.id
    );

    if (position == -1) return;

    this.CONTACT_SET({ position, contact });

    if ((await this.isCanBeInsertInMiddle({ contact })) == false) {
      this.CONTACTS_REMOVE_AT({ position });

      if (this.paginatedParams.hasNextPage == true) {
        const { pageIndex, pageSize } = this.paginatedParams;
        await this.fetchContacts({
          pageSize: 1,
          isPush: true,
          pageIndex:
            this.paginatedParams.pageSize *
              (this.paginatedParams.pageIndex + 1) -
            1
        });
        await this.reloadPaginatedParams({ pageIndex, pageSize });
      }
    }
  }

  @Action
  public async RemoveContact(contactId: string): Promise<void> {
    await this.apiProvider.Remove(contactId).then(async () => {
      const position = this.items.map(x => x.id).indexOf(contactId);

      if (position == -1) return;

      this.CONTACTS_REMOVE_AT({ position });
      const { pageIndex, pageSize } = this.paginatedParams;

      if (this.paginatedParams.hasNextPage == true) {
        await this.fetchContacts({
          pageSize: 1,
          isPush: true,
          pageIndex:
            this.paginatedParams.pageSize *
              (this.paginatedParams.pageIndex + 1) -
            1
        });
      }

      await this.reloadPaginatedParams({ pageIndex, pageSize });
      if (this.paginatedParams.pageIndex >= this.paginatedParams.totalPages)
        this.PAGINATED_PARAMS_SET({
          pageIndex: this.paginatedParams.totalPages - 1
        });
    });
  }

  @Action
  public async ReloadContacts(): Promise<void> {
    this.LOADING_STATE_SET({ isLoading: true });
    await this.fetchContacts({ pageIndex: 0, isPush: false }).finally(() => {
      this.LOADING_STATE_SET({ isLoading: false });
    });
  }

  @Action
  public async NextPage(): Promise<boolean> {
    if (!this.paginatedParams.hasNextPage) return false;

    await this.fetchContacts({
      pageIndex: this.paginatedParams.pageIndex + 1,
      isPush: true
    });

    return true;
  }

  @Action
  private isCanBeInsertInMiddle(payload: { contact: Contact }): boolean {
    const { contact } = payload;

    return (
      this.items.length <
        (this.paginatedParams.pageIndex + 1) * this.paginatedParams.pageSize ||
      this.items[this.items.length - 1].name.localeCompare(contact.name) > 0
    );
  }

  @Action
  private isItemsOverflow(): boolean {
    return (
      this.items.length >
      (this.paginatedParams.pageIndex + 1) * this.paginatedParams.pageSize
    );
  }

  @Action
  private async reloadPaginatedParams(payload: {
    pageIndex: number;
    pageSize: number;
  }) {
    return await this.apiProvider
      .GetAll(payload.pageIndex, payload.pageSize)
      .then(response => {
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
        payload.pageSize || this.paginatedParams.pageSize
      )
      .then(response => {
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

const ContactsModule = getModule(Contacts);

export default ContactsModule;
