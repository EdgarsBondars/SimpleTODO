<template>
    <div class="todo-component">
        <h1>To Do list</h1>

        <div v-if="loading" class="loading">
            Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationvue">https://aka.ms/jspsintegrationvue</a> for more details.
        </div>

        <div v-if="post" class="content">
            <table>
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Description</th>
                        <th>Completed?</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="forecast in post" :key="forecast.date">
                        <td>{{ forecast.title }}</td>
                        <td>{{ forecast.description }}</td>
                        <td>{{ forecast.isCompleted }}</td>
                        <td>
                            <button @click="deleteTodo(forecast.id)">Delete</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script lang="js">
    import { defineComponent } from 'vue';

    export default defineComponent({
        data() {
            return {
                loading: false,
                post: null
            };
        },
        created() {
            // fetch the data when the view is created and the data is
            // already being observed
            this.fetchData();
        },
        watch: {
            // call again the method if the route changes
            '$route': 'fetchData'
        },
        methods: {
            fetchData() {
                this.post = null;
                this.loading = true;

                fetch('https://localhost:7010/api/TodoItems')
                    .then(r => r.json())
                    .then(json => {
                        this.post = json;
                        this.loading = false;
                        return;
                    });
            },
            deleteTodo(todoId) {
                // Send delete request to API
                fetch(`https://localhost:7010/api/TodoItems/${todoId}`, {
                    method: 'DELETE'
                })
                    .then(() => {
                        // Remove the deleted todo item from the list
                        this.post = this.post.filter(todo => todo.id !== todoId);
                    })
                    .catch(error => {
                        console.error('Error deleting todo item:', error);
                    });
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

.todo-component {
    text-align: center;
}

table {
    margin-left: auto;
    margin-right: auto;
}
</style>