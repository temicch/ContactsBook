import Vue from "vue";
import Vuex from "vuex";

import { ContactsState } from "@/store/types";

Vue.use(Vuex);

export interface RootState {
    contacts: ContactsState;
}

export default new Vuex.Store<RootState>({
    strict: process.env.NODE_ENV !== "production",
});
