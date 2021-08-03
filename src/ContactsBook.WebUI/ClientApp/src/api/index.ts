import { isContainsOnlyDigits } from "@/utils";
import axios, { AxiosInstance } from "axios";

export default class ContactsApiProvider implements ApiProvider<Contact> {
  public static conduitApi: AxiosInstance = axios.create({
    baseURL: "https://192.168.1.212:45455/api",
  });

  async GetAll(
    pageIndex?: number,
    pageSize?: number,
    name?: string,
    phoneNumber?: string
  ): Promise<PaginatedList<Contact>> {
    const request: GetContactsRequest = {
      pageSize: pageSize,
      pageIndex: pageIndex,
    };

    if (name != null) request.name = name;
    if (phoneNumber != null && isContainsOnlyDigits(phoneNumber)) request.phoneNumber = phoneNumber;

    return await ContactsApiProvider.conduitApi
      .get("/contacts", {
        params: request,
      })
      .then((result) => result.data.response);
  }

  async Get(id: string): Promise<Contact> {
    const request: GetContactRequest = {
      id,
    };

    return await ContactsApiProvider.conduitApi
      .get(`/contacts/${request.id}`, {
        params: request,
      })
      .then((response) => response.data.response);
  }

  async Remove(contactId: string): Promise<void> {
    return await ContactsApiProvider.conduitApi.delete(
      `/contacts/${contactId}`
    );
  }

  async Create<CreateContactResponse>(
    contact: Contact
  ): Promise<CreateContactResponse> {
    return await ContactsApiProvider.conduitApi
      .post(`/contacts`, contact)
      .then((response) => response.data);
  }

  async Update(contact: Contact): Promise<void> {
    const request: UpdateContactsRequest = {
      ...contact,
    };

    return await ContactsApiProvider.conduitApi.put(
      `/contacts/${contact.id}`,
      request
    );
  }
}
