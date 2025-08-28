<template>
    <div class="weather-component">
        <h1>userdata</h1>
        <p>This component demonstrates fetching data from the server.</p>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div v-if="post" class="content">
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="userdata in post" :key="userdata.id">
                        <td>{{ userdata.id }}</td>
                        <td>{{ userdata.name }}</td>
                        <td>{{ userdata.email }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';

    // type Forecasts = {
    //     date: string,
    //     temperatureC: string,
    //     temperatureF: string,
    //     summary: string
    // }[];

    // interface Data {
    //     loading: boolean,
    //     post: null | Forecasts
    // }

    type userdatas = {
        id : number,
        name : string,
        email : string
    }[]

    interface Data {
        loading: boolean,
        post: null | userdatas
    }

    export default defineComponent({
        data(): Data {
            return {
                loading: false,

                post: null
            };
        },
        async created() {
            // fetch the data when the view is created and the data is
            // already being observed
            await this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            async fetchData() {
                this.post = null;
                this.loading = true;

                var response = await fetch('userdata');
                if (response.ok) {
                    this.post = await response.json();
                    this.loading = false;
                } else {
                    console.error('Failed to fetch data:', response.statusText);
                    this.loading = false;
                }
            }
        },
    });
</script>

<style scoped>
th {
    font-weight: bold;
}

th, td {
    padding-left: .5rem;
    padding-right: .5rem;
}

.weather-component {
    text-align: center;
}

table {
    margin-left: auto;
    margin-right: auto;
}
</style>