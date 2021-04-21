import { VuexModule, Module, Action, Mutation, getModule } from 'vuex-module-decorators';

import store from '@/store';
import { ContactsState } from '@/store/types';
import { Contact, PaginatedList, PaginatedParams } from '@/api/types';
import { getContacts } from '@/api';

const defaultPageSize = 20;

@Module({ dynamic: true, store, name: 'contacts' })
export class Contacts extends VuexModule implements ContactsState {
    loading = false;
    items: Contact[] = [];
    paginatedParams: Omit<PaginatedParams, "hasPreviousPage"> = {
        pageIndex: 0,
        pageSize: defaultPageSize,
        totalCount: 0,
        totalPages: 0,
        hasNextPage: false
    };

    @Mutation
    private SET_CONTACTS(payload: PaginatedList<Contact>) {
        this.items = payload.items;
    }

    @Mutation
    private PUSH_CONTACTS(payload: Contact[]) {
        this.items.push(...payload);
    }

    @Mutation
    private SET_PAGINATED_PARAMS(payload: PaginatedParams | PaginatedList<Contact>) {
        this.paginatedParams = { 
            hasNextPage: payload.hasNextPage, 
            pageIndex: payload.pageIndex, 
            pageSize: payload.pageSize, 
            totalPages: payload.totalPages, 
            totalCount: payload.totalCount 
        };
    }

    @Mutation
    private SET_LOADING_STATE(isLoading: boolean) {
        this.loading = isLoading;
    }

    @Action
    public async LoadContacts(): Promise<void> {
        this.SET_LOADING_STATE(true);
        await this.fetchContacts({ pageIndex: this.paginatedParams.pageIndex, isPush: false })
        .finally(() => {
            this.SET_LOADING_STATE(false);
        });
    }

    @Action
    public async ReloadContacts(): Promise<void> {
        this.SET_LOADING_STATE(true);
        await this.fetchContacts({ pageIndex: 0, isPush: false })
        .finally(() => {
            this.SET_LOADING_STATE(false);
        });
    }

    // @Action
    // public async RemoveContact(contactId: string): Promise<void> {
    //     await removeContact(contactId)
    //     .then(() => {

    //     });
    // }

    @Action
    public async NextPage(): Promise<boolean> {
        if(!this.paginatedParams.hasNextPage)
            return false;
            
        await this.fetchContacts({ pageIndex: this.paginatedParams.pageIndex + 1, isPush: true });

        return true;
    }

    @Action
    private async fetchContacts(payload: { pageIndex: number, isPush: boolean }): Promise<void> { 
        await getContacts(payload.pageIndex, this.paginatedParams.pageSize)
        .then(response => {
            if(!payload.isPush) {
                this.SET_CONTACTS(response);
                this.SET_PAGINATED_PARAMS(response);
            } else {
               this.PUSH_CONTACTS(response.items);
               this.SET_PAGINATED_PARAMS(response);
            }
        });
    }

    @Action 
    private async fetchLastContact() {
        await getContacts(this.paginatedParams.pageIndex * this.paginatedParams.pageSize, 1)
        .then(response => {
            this.PUSH_CONTACTS(response.items);
        });
    }

    get isLoading(): boolean {
        return this.loading;
    }

    get contacts(): Contact[] {
        return this.items;
    }
}

export const ContactsModule = getModule(Contacts)
