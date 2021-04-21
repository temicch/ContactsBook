export interface GetContactsResponse {
    response: PaginatedList<Contact>;
}

export interface GetContactsRequest {
    phoneNumber?: string,
    name?: string,
    pageSize?: number,
    pageIndex?: number
}

export interface UpdateContactsRequest {
    id: string,
    name: string,
    phoneNumber: number,
    email?: string
}

export interface Contact {
    id: string,
    name: string,
    phoneNumber: number,
    email?: string
}

export interface PaginatedParams {
    pageIndex: number,
    pageSize: number,
    totalCount: number,
    totalPages: number,
    hasPreviousPage: boolean,
    hasNextPage: boolean
}

export type PaginatedList<Type> = PaginatedParams & {
    items: Type[],
}