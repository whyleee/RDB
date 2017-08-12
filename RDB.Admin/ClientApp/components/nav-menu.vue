<template>
  <v-list>
    <v-list-tile
      v-for="item in items"
      :key="item.url"
      :to="item.to"
    >
      <v-list-tile-action>
        <v-icon>{{item.icon}}</v-icon>
      </v-list-tile-action>
      <v-list-tile-content>
        <v-list-tile-title>{{item.title}}</v-list-tile-title>
      </v-list-tile-content>
    </v-list-tile>
  </v-list>
</template>

<script>
export default {
  data() {
    return {
      items: [
        { icon: 'home', title: 'Home', to: '/' },
        { icon: 'exposure_plus_1', title: 'Counter', to: '/counter' },
        { icon: 'list', title: 'Values', to: '/values' }
      ]
    }
  }
}
</script>
