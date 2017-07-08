<template>
  <div id="app-root">
    <nav-menu />
    <router-view />
  </div>
</template>

<script>
import NavMenu from './nav-menu.vue'

export default {
  components: {
    NavMenu
  }
}
</script>