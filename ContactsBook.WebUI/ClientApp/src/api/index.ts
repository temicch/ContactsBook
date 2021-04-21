import axios from 'axios';

import { Contact, GetContactsRequest, PaginatedList, UpdateContactsRequest } from './types';

const conduitApi = axios.create({
    baseURL: 'http://localhost:56544/api',
  });

export async function getContacts (pageIndex?: number, pageSize?: number): Promise<PaginatedList<Contact>> {
    const requestParams: GetContactsRequest = {
        pageSize: pageSize,
        pageIndex: pageIndex,
    };

    const request = await conduitApi.get('/contacts', {
        params: requestParams
    });

    return request.data.response;
}

export async function removeContact (contactId: string): Promise<void> {
    await conduitApi.delete(`/contacts/${contactId}`);
}

export async function updateContact (contact: Contact): Promise<void> {
    const request: UpdateContactsRequest = {
        ...contact
    }
    await conduitApi.put(`/contacts/${request.id}`, request);
}
