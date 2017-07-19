<template>
  <div>
    <h1>Values</h1>
    <p>This component fetches values from api.</p>

    <p v-if="!values"><em>Loading...</em></p>
    <ul v-else>
      <li v-for="value in values" :key="value.id">
        [{{value.id}}] {{value.text}}
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      values: null
    }
  },
  async created() {
    let response = await axios.get('http://localhost:18420/api/values')
    this.values = response.data
  }
}
</script>
