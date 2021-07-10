interface GetContactsResponse {
  response: PaginatedList<Contact>;
}

interface GetContactsRequest {
  phoneNumber?: string;
  name?: string;
  pageSize?: number;
  pageIndex?: number;
}

interface GetContactRequest {
  id: string;
}

interface UpdateContactsRequest {
  id: string;
  name: string;
  phoneNumber: number;
  email?: string;
}

interface CreateContactRequest {
  name: string;
  phoneNumber: number;
  email?: string;
}

interface CreateContactResponse {
  id: string;
}

interface Contact {
  id: string;
  name: string;
  phoneNumber: number;
  email?: string;
}

interface PaginatedParams {
  pageIndex: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

interface ApiProvider<T> {
  Get(id: string): Promise<T>;
  GetAll(pageIndex?: number, pageSize?: number, name?: string, phoneNumber?: string): Promise<PaginatedList<T>>;
  Remove(id: string): Promise<void>;
  Create<Response>(item: T): Promise<Response>;
  Update(item: T): Promise<void>;
}

type PaginatedList<Type> = PaginatedParams & {
  items: Type[];
};
