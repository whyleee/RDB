<template>
  <v-layout>
    <v-flex xs12 md9>
      <v-card>
        <v-card-title class="green white--text">
          <div class="headline">Values</div>
        </v-card-title>
        <v-card-text>
          <p>This component fetches values from RDB api.</p>
          <v-subheader>GET /api/values</v-subheader>
          <p v-if="!values"><em>Loading...</em></p>
          <v-list v-else>
            <v-list-tile
              v-for="value in values"
              :key="value.id"
            >
              <v-list-tile-action>
                <v-icon>label</v-icon>
              </v-list-tile-action>
              <v-list-tile-title>{{value.text}}</v-list-tile-title>
              <v-list-tile-sub-title>ID: {{value.id}}</v-list-tile-sub-title>
            </v-list-tile>
          </v-list>
        </v-card-text>
      </v-card>
    </v-flex>
  </v-layout>
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
