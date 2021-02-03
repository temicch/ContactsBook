import Vue from 'vue';
import Avatar from 'vue-avatar';
import axios from 'axios';
import VueFinalModal from 'vue-final-modal'
import 'normalize.css';

import './styles/index.scss';

Vue.use(VueFinalModal())

new Vue({
    el: "#main",
    data() {
        return {
            loading: true,
            contacts: [],
            modalPlugin: null,
            showModal: false,
        }
    },
    components: {
        Avatar,
    },
    async mounted() {
        await axios.get('/api/contacts/all')
            .then(response => {
                this.contacts = response.data.sort((a, b) => a.Name.localeCompare(b.Name));
            })
            .finally(() =>
                this.loading = false);
        this.loading = false
        setTimeout(() => {
            // var elems = document.querySelectorAll('.modal');
            // this.modalPlugin = M.Modal.init(elems);
            // console.log(this.$refs);
        }, 0);
    },
    methods: {
        async removeContact(value)
        {
            console.log(value);
            // await axios.delete('/api/contacts', {id: value.Id});
        }
    },
    filters: {
        phoneNumber: function (value) {
            let i = 0;
            value = '+# (###) ### ## ##'.replace(/#/g, _ => value[i++]);
            return value;
        }
    },
    destroyed() {
        // if (this.modalPlugin && this.modalPlugin.destroy) {
        //   this.modalPlugin.destroy()
        // }
    }
});