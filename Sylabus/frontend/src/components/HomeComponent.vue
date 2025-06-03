<template>
  <div class="d-flex justify-content-center align-items-center w-100 p-4 min-vh-100" style="background-color: #1e2a47;">
    <div class="card p-4 shadow-lg rounded" style="max-width: 400px; width: 100%; background-color: rgba(255, 255, 255, 0.9);">
      <div class="text-center mb-4">
        <img src="@/assets/logo.png" alt="Społeczna Akademia Nauk Logo" class="mx-auto mb-3" style="max-height: 64px; width: auto;" />
      </div>

      <b-tabs v-model="activeTab" class="mb-4">
        <b-tab title="Logowanie" active>
          <form @submit.prevent="handleSubmit">
            <div class="mb-3">
              <label for="email" class="form-label text-dark">Adres e-mail</label>
              <input type="email" id="email" class="form-control" v-model="email" placeholder="Wpisz adres e-mail" required />
            </div>
            <div class="mb-3">
              <label for="password" class="form-label text-dark">Hasło</label>
              <input type="password" id="password" class="form-control" v-model="password" placeholder="Wpisz hasło" required />
            </div>
            <button type="submit" class="btn btn-primary w-100" style="background-color: #00468c; border-color: #00468c;">
              Zaloguj się
            </button>
          </form>
        </b-tab>
        <b-tab title="Rejestracja">
          <form @submit.prevent="handleSubmit">
            <div class="mb-3">
              <label for="firstName" class="form-label text-dark">Imię</label>
              <input type="text" id="firstName" class="form-control" v-model="firstName" placeholder="Wpisz imię" required />
            </div>
            <div class="mb-3">
              <label for="lastName" class="form-label text-dark">Nazwisko</label>
              <input type="text" id="lastName" class="form-control" v-model="lastName" placeholder="Wpisz nazwisko" required />
            </div>
            <div class="mb-3">
              <label for="email" class="form-label text-dark">Adres e-mail</label>
              <input type="email" id="email" class="form-control" v-model="email" placeholder="Wpisz adres e-mail" required />
            </div>
            <div class="mb-3">
              <label for="password" class="form-label text-dark">Hasło</label>
              <input type="password" id="password" class="form-control" v-model="password" placeholder="Wpisz hasło" required />
            </div>
            <div class="mb-3">
              <label for="confirmPassword" class="form-label text-dark">Potwierdź hasło</label>
              <input type="password" id="confirmPassword" class="form-control" v-model="confirmPassword" placeholder="Potwierdź hasło" required />
            </div>
            <button type="submit" class="btn btn-primary w-100" style="background-color: #00468c; border-color: #00468c;">
              Zarejestruj się
            </button>
          </form>
        </b-tab>
      </b-tabs>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
export default {
  data() {
    return {
      email: '',
      password: '',
      firstName: '',
      lastName: '',
      confirmPassword: '',
      activeTab: 0,
    };
  },
  computed: {
    isLogin() {
      return this.activeTab === 0;
    },
  },
  methods: {
    async handleSubmit() {
      if (this.isLogin) {
        // LOGOWANIE
        try {
          const response = await axios.post('/login', {
            email: this.email,
            password: this.password
          });

          alert(`Zalogowano! Użytkownik ID: ${response.data.id}, Email: ${response.data.email}`);
          window.location = '/sylabus';
        } catch (error) {
          alert(error.response?.data?.message || 'Błąd logowania');
        }

      } else {
        // REJESTRACJA
        if (this.password !== this.confirmPassword) {
          alert('Hasła się nie zgadzają!');
          return;
        }

        try {
          const response = await axios.post('/register', {
            firstName: this.firstName,
            lastName: this.lastName,
            email: this.email,
            password: this.password
          });

          alert(`Zarejestrowano! ` + response?.data?.message);
        } catch (error) {
          alert(error.response?.data?.message || 'Błąd rejestracji');
        }
      }
    },
  },
};
</script>

<style scoped>
/* Ensure the container takes the full viewport height */
.min-vh-100 {
  min-height: 100vh;
}

/* Custom font with fallback */
body, .form-label, .form-control {
  font-family: 'Rubik', sans-serif;
}

/* Remove bold from labels */
.form-label {
  font-weight: normal;
  color: #34495e;
}

/* Optional hover effect for button */
.btn-primary:hover {
  background-color: #00345c !important;
  border-color: #00345c !important;
}

/* Form inputs */
.form-control {
  border-radius: 0.375rem;
}

/* Card padding for smaller screens */
@media (max-width: 576px) {
  .card {
    padding: 1.5rem;
  }
}
</style>