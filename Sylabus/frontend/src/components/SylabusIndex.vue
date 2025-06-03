<template>
  <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;600&display=swap" rel="stylesheet">
  <div class="bg-light text-dark min-vh-100" style="font-family: 'Poppins', sans-serif">
    <!-- Filtry -->
    <div class="container py-4">
      <div class="row g-3">
        <div class="col-md-3">
          <label class="form-label">Ośrodek</label>
          <select class="form-select" v-model="selectedCenter">
            <option>Wszystkie</option>
            <option>SAN Warszawa</option>
            <option>SAN Łódź</option>
          </select>
        </div>
        <div class="col-md-3">
          <label class="form-label">Rok akademicki</label>
          <select class="form-select" v-model="selectedYear">
            <option>2024/2025</option>
            <option>2023/2024</option>
            <option>2022/2023</option>
          </select>
        </div>
        <div class="col-md-3">
          <label class="form-label">Semestr</label>
          <select class="form-select" v-model="selectedSemester">
            <option v-for="i in 7" :key="i">Semestr {{ i }}</option>
            <option>Wszystkie</option>
          </select>
        </div>
        <div class="col-md-3">
          <label class="form-label">Wydział</label>
          <select class="form-select" v-model="faculty">
            <option>Informatyka</option>
            <option>Pedagogika</option>
            <option>Zarządzanie</option>
            <option>Prawo</option>
          </select>
        </div>
      </div>

      <div class="mt-4 d-flex justify-content-between align-items-center">
        <h4 class="mb-0">📚 Lista sylabusów</h4>
        <input class="form-control w-50" placeholder="🔍 Szukaj przedmiotu..." v-model="search" />
      </div>
    </div>

    <!-- Lista Sylabusów -->
    <div class="container py-3">
      <div v-if="syllabuses.length === 0" class="text-muted">Brak wyników.</div>
      <div v-else class="row">
        <div v-for="item in filteredSyllabuses" :key="item.id" class="col-md-6 col-lg-4 mb-4">
          <div class="card shadow-sm h-100">
            <div class="card-body">
              <h5 class="card-title">{{ item.name }}</h5>
              <p class="card-text">{{ item.description }}</p>
              <small class="text-muted">Wydział: {{ item.faculty }} | Semestr: {{ item.semester }}</small>
              <div class="mt-3 d-flex justify-content-between">
                <button class="btn btn-sm btn-outline-primary">Edytuj</button>
                <span class="text-muted">{{ item.hours }}h</span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "SylabusIndex",
  data() {
    return {
      selectedCenter: 'Wszystkie',
      selectedYear: '2024/2025',
      selectedSemester: 'Semestr 1',
      faculty: 'Informatyka',
      search: '',
      syllabuses: [
        {
          id: 1,
          name: 'Algorytmy i Struktury Danych',
          description: 'Przedmiot z zakresu analizy algorytmów.',
          faculty: 'Informatyka',
          semester: '1',
          hours: 45,
        },
        {
          id: 2,
          name: 'Psychologia rozwoju',
          description: 'Podstawy rozwoju dziecka i młodzieży.',
          faculty: 'Pedagogika',
          semester: '2',
          hours: 30,
        },
      ],
    };
  },
  computed: {
    filteredSyllabuses() {
      return this.syllabuses.filter((s) =>
          s.name.toLowerCase().includes(this.search.toLowerCase())
      );
    },
  },
  methods: {
    goToSettings() {
      alert("Przejście do ustawień użytkownika");
    },
  },
};
</script>

<style scoped>
body {
  font-family: 'Poppins', sans-serif;
  background-color: #f5f5f5;
  color: #333;
}

.navbar {
  background-color: #1e2a3a;
}

.navbar .nav-link {
  color: #ffffff;
  font-weight: 500;
}

.navbar .nav-link:hover {
  color: #b8c7dd;
}

.btn-outline-light:hover {
  background-color: #ffffff;
  color: #1e2a3a;
  border-color: #ffffff;
}

.card {
  font-size: 0.95rem;
  transition: all 0.2s ease-in-out;
}

.card:hover {
  transform: scale(1.01);
  box-shadow: 0 0 10px rgba(0,0,0,0.1);
}

label, .form-select, .btn {
  font-size: 0.95rem;
}
</style>
