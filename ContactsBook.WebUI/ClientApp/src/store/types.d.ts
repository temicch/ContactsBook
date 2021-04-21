import { Contact, PaginatedParams } from "@/api/types";

export type ContactsState = {
    items: Contact[],
    paginatedParams: Omit<PaginatedParams, "hasPreviousPage">,
    loading: boolean
}