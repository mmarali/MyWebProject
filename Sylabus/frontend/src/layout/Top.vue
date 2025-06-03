<template>
  <div class="top-bar">
    <nav class="navbar navbar-expand-lg navbar-dark shadow-sm px-4" style="background-color: #1e2a3a;">
      <div class="container-fluid">
        <a class="navbar-brand d-flex align-items-center" href="#">
          <img src="@/assets/logo.png" alt="Logo" height="40" />
        </a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navLinks">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navLinks">
          <ul class="navbar-nav mx-auto mb-2 mb-lg-0">
            <li class="nav-item"><a class="nav-link text-white" href="dodaj-sylabus">Dodaj Sylabus</a></li>
            <li class="nav-item"><a class="nav-link text-white" href="sylabus">Lista Sylabusów</a></li>
            <li class="nav-item"><a class="nav-link text-white" href="uczelnie">Uczelnie</a></li>
            <li class="nav-item"><a class="nav-link text-white" href="#">Wydziały</a></li>
          </ul>
          <button class="btn btn-outline-light d-flex align-items-center" @click="goToSettings">
            <i class="fa fa-user-cog me-2"></i> Ustawienia
          </button>
        </div>
      </div>
    </nav>
  </div>


</template>

<script>
export default {
  name: 'TopLayout',
};
</script>