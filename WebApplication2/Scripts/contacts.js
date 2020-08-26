new Vue({
    el: "#main",
    data: () => ({
        loading: true,
        contacts: [
            {
                Name: "Nastya Bayda",
                Email: "jopa@ruchka.com"
            },
            {
                Name: "Temich",
                Email: "xuy@ckrayu.com"
            }],
        
    }),
    async mounted() {
        this.loading = false
    },
    methods: {

    },
  })