<template>
  <TopLayout v-if="currentComponent !== HomeComponent" />
  <div>
    <component :is="currentComponent"></component>
  </div>
</template>

<script>
import PrivacyComponent from './components/PrivacyComponent.vue';
import SylabusIndex from "@/components/SylabusIndex.vue";
import AddSylabus from "@/components/AddSylabus.vue";
import TopLayout from "@/layout/Top.vue";
import HomeComponent from "@/components/HomeComponent.vue";
import AcademiesIndex from "@/components/AcademiesIndex.vue";

export default {
  name: 'App',
  components: {TopLayout},
  computed: {
    HomeComponent() {
      return HomeComponent
    },
    currentComponent() {
      const componentName = window.vueComponent;
      return {
        HomeComponent,
        PrivacyComponent,
        SylabusIndex,
        AddSylabus,
        AcademiesIndex
      }[componentName];
    }
  }
};
</script>