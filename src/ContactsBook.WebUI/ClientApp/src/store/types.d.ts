type ContactsState = {
  items: Contact[];
  paginatedParams: PaginatedParams;
  loading: boolean;
};
type ContactsSearchState = ContactsState & {
  searchPhrase: string;
};
