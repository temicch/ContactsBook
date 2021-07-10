import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

export interface RootState {
    contacts: ContactsState;
    contactsSearch: ContactsSearchState;
}

export default new Vuex.Store<RootState>({
    strict: process.env.NODE_ENV !== "production",
});
