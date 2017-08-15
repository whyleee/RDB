<template>
  <v-app toolbar>
    <v-navigation-drawer
      persistent
      clipped
      enable-resize-watcher
      v-model="drawer"
    >
      <nav-menu></nav-menu>
    </v-navigation-drawer>
    <v-toolbar fixed>
      <v-toolbar-side-icon @click.native.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title v-text="title"></v-toolbar-title>
    </v-toolbar>
    <main>
      <v-container fluid>
        <router-view></router-view>
      </v-container>
    </main>
  </v-app>
</template>

<script>
import NavMenu from './nav-menu.vue'

export default {
  components: {
    NavMenu
  },
  data() {
    return {
      drawer: true,
      title: 'RDB Admin'
    }
  }
}
</script>
