<template>
  <div class="container py-5" id="app" :class="{ 'dark-mode': formData.darkMode }">
    <h2 class="mb-4 text-center">📝 Dodaj Nowy Sylabus</h2>
    <button @click="formData.darkMode = !formData.darkMode" class="btn btn-outline-primary mb-4">
      {{ formData.darkMode ? 'Tryb jasny' : 'Tryb ciemny' }}
    </button>

    <form @submit.prevent="submitForm" class="bg-white p-5 rounded shadow-sm" :class="{ 'bg-dark text-white': formData.darkMode }">
      <!-- I. Ogólne informacje -->
      <div class="mb-4">
        <h4 class="border-bottom pb-2 mb-3">I. Ogólne informacje</h4>
        <div class="row">
          <div class="col-md-6 mb-3">
            <label for="name" class="form-label">Nazwa przedmiotu</label>
            <input type="text" id="name" v-model="formData.name" class="form-control" required />
          </div>
          <div class="col-md-6 mb-3">
            <label for="code" class="form-label">Kod przedmiotu</label>
            <input type="text" id="code" v-model="formData.code" class="form-control" required />
          </div>
        </div>
        <div class="row">
          <div class="col-md-6 mb-3">
            <label class="form-label">Nazwa jednostki organizacyjnej prowadzącej kierunek</label>
            <input type="text" v-model="formData.unit" class="form-control" />
          </div>
          <div class="col-md-6 mb-3">
            <label class="form-label">Nazwa kierunku studiów, poziom kształcenia</label>
            <input type="text" v-model="formData.major" class="form-control" />
          </div>
        </div>
        <div class="row">
          <div class="col-md-4 mb-3">
            <label class="form-label">Profil kształcenia</label>
            <input type="text" v-model="formData.profile" class="form-control" />
          </div>
          <div class="col-md-4 mb-3">
            <label class="form-label">Nazwa specjalności</label>
            <input type="text" v-model="formData.specialty" class="form-control" />
          </div>
          <div class="col-md-4 mb-3">
            <label class="form-label">Rodzaj modułu kształcenia</label>
            <input type="text" v-model="formData.moduleType" class="form-control" />
          </div>
        </div>
        <div class="row">
          <div class="col-md-4 mb-3">
            <label class="form-label">Punkty ECTS</label>
            <input type="number" v-model.number="formData.ects" class="form-control" min="0" />
          </div>
          <div class="col-md-4 mb-3">
            <label class="form-label">Rok / semestr</label>
            <input type="text" v-model="formData.yearSemester" class="form-control" />
          </div>
          <div class="col-md-4 mb-3">
            <label class="form-label">Koordynator</label>
            <input type="text" v-model="formData.coordinator" class="form-control" />
          </div>
        </div>
      </div>

      <!-- Wymagania wstępne -->
      <div class="mb-4">
        <h4>II. Wymagania wstępne (wynikające z następstwa przedmiotu)</h4>
        <div v-for="(item, index) in formData.requirementsList" :key="index" class="mb-2 d-flex">
          <input
              v-model="formData.requirementsList[index]"
              type="text"
              class="form-control"
              :class="{ 'bg-dark text-white': formData.darkMode }"
              placeholder="Wpisz wymaganie"
          />
          <button type="button" class="btn btn-danger btn-sm ms-2" @click="removeRequirement(index)">✖</button>
        </div>
        <button type="button" @click="formData.requirementsList.push('')" class="btn btn-outline-primary btn-sm mt-2">Dodaj punkt</button>
      </div>

      <!-- Cele kształcenia -->
      <div class="mb-4">
        <h4 class="border-bottom pb-2 mb-3">III. Cele kształcenia dla przedmiotu</h4>
        <table class="table" :class="{ 'table-dark': formData.darkMode }">
          <thead>
          <tr>
            <th style="width: 60px;">Nr</th>
            <th>Cel kształcenia</th>
            <th style="width: 40px;">Usuń</th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="(goal, index) in formData.goalsList" :key="index">
            <td><strong>C{{ index + 1 }}</strong></td>
            <td>
              <input
                  v-model="formData.goalsList[index]"
                  type="text"
                  class="form-control"
                  placeholder="Wpisz cel kształcenia"
                  :class="{ 'bg-dark text-white': formData.darkMode }"
              />
            </td>
            <td>
              <button
                  class="btn btn-danger btn-sm"
                  @click="removeGoal(index)"
                  aria-label="Usuń cel"
              >✖</button>
            </td>
          </tr>
          </tbody>
        </table>
        <button type="button" class="btn btn-outline-primary btn-sm" @click="addGoal">➕ Dodaj cel</button>
      </div>

      <!-- formy zajec dydaktrycznych oraz wymiar godzin -->

      <div class="mb-4">
        <h4 class="border-bottom pb-2 mb-3">IV. Formy zajęć dydaktycznych oraz wymiar godzin</h4>
        <table class="table" :class="{ 'table-dark': formData.darkMode }">
          <thead>
          <tr>
            <th>Typ</th>
            <th>Liczba godzin</th>
            <th>Tryb studiów</th>
            <th></th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="(activity, index) in formData.teachingForms" :key="index">
            <td>
              <select v-model="activity.type" class="form-select" :class="{ 'bg-dark text-white': formData.darkMode }">
                <option disabled value="">Wybierz...</option>
                <option>Wykład</option>
                <option>Ćwiczenia</option>
                <option>Laboratorium</option>
                <option>Projekt</option>
                <option>Seminarium</option>
                <option>Praktyka</option>
              </select>
            </td>
            <td>
              <input type="number" min="0" v-model="activity.hours" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" />
            </td>
            <td>
              <select v-model="activity.mode" class="form-select" :class="{ 'bg-dark text-white': formData.darkMode }">
                <option disabled value="">Wybierz...</option>
                <option>Stacjonarne</option>
                <option>Niestacjonarne</option>
              </select>
            </td>
            <td>
              <button class="btn btn-danger btn-sm" @click="removeTeachingForm(index)">✖</button>
            </td>
          </tr>
          </tbody>
        </table>
        <button type="button" class="btn btn-outline-primary btn-sm" @click="addTeachingForm">➕ Dodaj formę zajęć</button>
      </div>

      <!-- Metody dydaktyczne -->
      <div class="mb-4">
        <h4 class="border-bottom pb-2 mb-3">V. Metody dydaktyczne</h4>
        <table class="table" :class="{ 'table-dark': formData.darkMode }">
          <thead>
          <tr>
            <th>Formy zajęć</th>
            <th>Metody dydaktyczne</th>
            <th></th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="(method, index) in formData.teachingMethods" :key="index">
            <td>
              <select v-model="method.type" class="form-select" :class="{ 'bg-dark text-white': formData.darkMode }">
                <option disabled value="">Wybierz...</option>
                <option value="wyklad">Wykład</option>
                <option value="cwiczenia">Ćwiczenia / Projekt</option>
                <option value="praca_wlasna">Praca własna</option>
              </select>
            </td>
            <td><input v-model="method.name" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" /></td>
            <td><button class="btn btn-danger btn-sm" @click="removeTeachingMethod(index)">✖</button></td>
          </tr>
          </tbody>
        </table>
        <button type="button" class="btn btn-outline-primary btn-sm" @click="addTeachingMethod">➕ Dodaj metodę</button>
      </div>

      <!-- Efekty uczenia się -->
      <div class="mb-4">
        <h4 class="border-bottom pb-2 mb-3">VI. Przedmiotowe efekty uczenia się z odniesieniem do efektów uczenia się dla kierunku</h4>
        <table class="table" :class="{ 'table-dark': formData.darkMode }">
          <thead>
          <tr>
            <th>Numer </th>
            <th>Typ</th>
            <th>Opis </th>
            <th>Odniesienie</th>
            <th></th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="(effect, index) in formData.outcomes" :key="index">
            <td><input v-model="effect.number" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" placeholder="Np. W1" /></td>
            <td>
              <select v-model="effect.type" class="form-select" :class="{ 'bg-dark text-white': formData.darkMode }">
                <option disabled value="">Wybierz...</option>
                <option value="Wiedza">Wiedza</option>
                <option value="Umiejętności">Umiejętności</option>
                <option value="Kompetencje społeczne">Kompetencje społeczne</option>
              </select>
            </td>
            <td><input v-model="effect.description" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" /></td>
            <td><input v-model="effect.reference" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" placeholder="Np. K_W06" /></td>
            <td><button class="btn btn-danger btn-sm" @click="removeOutcome(index)">✖</button></td>
          </tr>
          </tbody>
        </table>
        <button type="button" class="btn btn-outline-primary btn-sm" @click="addOutcome">➕ Dodaj efekt</button>
      </div>

      <!-- Treści ksztalcenia -->
      <div class="mb-4">
        <h4 class="border-bottom pb-2 mb-3">VII. Treści kształcenia</h4>
        <table class="table" :class="{ 'table-dark': formData.darkMode }">
          <thead>
          <tr>
            <th>Numer</th>
            <th>Forma zajęć</th>
            <th>Temat</th>
            <th>Odniesienie</th>
            <th></th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="(item, index) in formData.content" :key="index">
            <td><input v-model="item.number" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" placeholder="Np. W1" /></td>
            <td>
              <select v-model="item.type" class="form-select" :class="{ 'bg-dark text-white': formData.darkMode }">
                <option disabled value="">Wybierz...</option>
                <option value="wyklad">Wykład</option>
                <option value="cwiczenia">Ćwiczenia / Projekt</option>
                <option value="praca_wlasna">Praca własna</option>
              </select>
            </td>
            <td><input v-model="item.topic" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" /></td>
            <td><input v-model="item.effect" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" placeholder="Np. P_W01" /></td>
            <td><button class="btn btn-danger btn-sm" @click="removeContent(index)">✖</button></td>
          </tr>
          </tbody>
        </table>
        <button type="button" class="btn btn-outline-primary btn-sm" @click="addContent">➕ Dodaj treść</button>
      </div>

      <!-- Formy i metody oceniania -->
      <div class="mb-4">
        <h4 class="border-bottom pb-2 mb-3">VIII. Formy i metody oceniania</h4>
        <table class="table" :class="{ 'table-dark': formData.darkMode }">
          <thead>
          <tr>
            <th>Numer </th>
            <th>Metoda weryfikacji</th>
            <th>Ocena niedostateczna<br>(0-49% / poniżej progu)</th>
            <th>Ocena dostateczna<br>(50-59% / powyżej progu)</th>
            <th>Ocena dobra<br>(60-74% / powyżej progu)</th>
            <th>Ocena bardzo dobra<br>(75-100% / powyżej progu)</th>
            <th></th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="(effect, index) in formData.effects" :key="index">
            <td>
              <input
                  v-model="effect.number"
                  class="form-control"
                  :class="{ 'bg-dark text-white': formData.darkMode }"
                  placeholder="Np. P_W01"
                  required
              />
            </td>
            <td>
              <input
                  v-model="effect.method"
                  class="form-control"
                  :class="{ 'bg-dark text-white': formData.darkMode }"
                  placeholder="Np. Egzamin pisemny"
                  required
              />
            </td>
            <td>
              <input
                  v-model="effect.gradeBelow50"
                  type="text"
                  class="form-control"
                  :class="{ 'bg-dark text-white': formData.darkMode }"
                  placeholder="Np. Brak opanowania materiału"
                  required
              />
            </td>
            <td>
              <input
                  v-model="effect.grade50_59"
                  type="text"
                  class="form-control"
                  :class="{ 'bg-dark text-white': formData.darkMode }"
                  placeholder="Np. Podstawowa znajomość materiału"
                  required
              />
            </td>
            <td>
              <input
                  v-model="effect.grade60_74"
                  type="text"
                  class="form-control"
                  :class="{ 'bg-dark text-white': formData.darkMode }"
                  placeholder="Np. Dobra znajomość materiału"
                  required
              />
            </td>
            <td>
              <input
                  v-model="effect.grade75_100"
                  type="text"
                  class="form-control"
                  :class="{ 'bg-dark text-white': formData.darkMode }"
                  placeholder="Np. Pełna znajomość materiału"
                  required
              />
            </td>
            <td>
              <button class="btn btn-danger btn-sm" @click="removeEffect(index)">✖</button>
            </td>
          </tr>
          </tbody>
        </table>
        <div class="mb-3">
          <button type="button" class="btn btn-outline-primary btn-sm" @click="addEffect">➕ Dodaj efekt uczenia się</button>
        </div>
        <p :class="{'text-danger': totalAssessmentWeight !== 100}">
          Suma wag: <strong>{{ totalAssessmentWeight }}%</strong> (powinna wynosić 100%)
        </p>
      </div>
      <!-- Nakład pracy studenta -->
      <div class="mb-4">
        <h4 class="border-bottom pb-2 mb-3">IX. Nakład pracy studenta - wymiar godzin i bilans punktów ECTS)</h4>
        <table class="table" :class="{ 'table-dark': formData.darkMode }">
          <thead>
          <tr>
            <th>Forma zajęć</th>
            <th>Godziny</th>
            <th></th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="(work, index) in formData.workload" :key="index">
            <td>
              <select v-model="work.type" class="form-select" :class="{ 'bg-dark text-white': formData.darkMode }">
                <option disabled value="">Wybierz...</option>
                <option value="wyklad">Wykład</option>
                <option value="cwiczenia">Ćwiczenia / Projekt</option>
                <option value="praca_wlasna">Praca własna</option>
              </select>
            </td>
            <td><input v-model.number="work.hours" type="number" min="0" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" /></td>
            <td><button class="btn btn-danger btn-sm" @click="removeWorkload(index)">✖</button></td>
          </tr>
          </tbody>
        </table>
        <button type="button" class="btn btn-outline-primary btn-sm" @click="addWorkload">➕ Dodaj nakład pracy</button>
      </div>

      <!-- Literatura -->
      <div class="mb-4">
        <h4 class="border-bottom pb-2 mb-3">X. Literatura przedmiotu oraz inne materiały dydaktyczne</h4>
        <table class="table" :class="{ 'table-dark': formData.darkMode }">
          <thead>
          <tr>
            <th>Nazwa literatury</th>
            <th>Typ literatury</th>
            <th></th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="(book, index) in formData.literature" :key="index">
            <td><input v-model="book.name" class="form-control" :class="{ 'bg-dark text-white': formData.darkMode }" /></td>
            <td>
              <select v-model="book.type" class="form-select" :class="{ 'bg-dark text-white': formData.darkMode }">
                <option value="" disabled>Wybierz typ...</option>
                <option value="podstawowa">Podstawowa</option>
                <option value="uzupelniajaca">Uzupełniająca</option>
                <option value="dydaktyczna">Dydaktyczna</option>
              </select>
            </td>
            <td><button class="btn btn-danger btn-sm" @click="removeLiterature(index)">✖</button></td>
          </tr>
          </tbody>
        </table>
        <button type="button" class="btn btn-outline-primary btn-sm" @click="addLiterature">➕ Dodaj książkę</button>
      </div>


      <!-- Formularz akcje -->
      <div class="d-flex justify-content-between">
        <button type="submit" class="btn btn-success" :disabled="!canSubmit">Zapisz sylabus</button>
        <button type="button" class="btn btn-secondary" @click="cancel">Anuluj</button>
        <button type="button" class="btn btn-info" @click="print">Drukuj sylabus</button>
      </div>
    </form>
  </div>
</template>

<script>
export default {
  name: "AddSyllabus",
  data() {
    return {
      formData: {
        darkMode: false,
        effects: [
          { number: '', method: '', gradeBelow50: '', grade50_59: '', grade60_74: '', grade75_100: '' }
        ],
        name: '',
        code: '',
        unit: '',
        major: '',
        profile: '',
        specialty: '',
        moduleType: '',
        ects: null,
        yearSemester: '',
        coordinator: '',
        requirementsList: [''],
        goalsList: [''],
        outcomes: [],
        content: [],
        teachingMethods: [],
        assessments: [],
        literature: [
          { name: '', type: '' }
        ],
        workload: [],
        teachingForms: [
          { type: '', hours: '', mode: '' }
        ],
      }
    };
  },
  computed: {
    totalAssessmentWeight() {
      return this.formData.assessments.reduce((sum, a) => sum + (Number(a.weight) || 0), 0);
    },
    canSubmit() {
      // Podstawowa walidacja:
      return (
          this.formData.name.trim() !== '' &&
          this.formData.code.trim() !== '' &&
          this.totalAssessmentWeight === 100
      );
    }
  },
  methods: {
    addGoal() {
      this.formData.goalsList.push('');
    },
    removeGoal(index) {
      this.formData.goalsList.splice(index, 1);
    },
    addOutcome() {
      this.formData.outcomes.push({ type: '', description: '', reference: '' });
    },
    removeOutcome(index) {
      this.formData.outcomes.splice(index, 1);
    },
    addContent() {
      this.formData.content.push({ type: '', number: '', topic: '', effect: '' });
    },
    removeContent(index) {
      this.formData.content.splice(index, 1);
    },
    addTeachingMethod() {
      this.formData.teachingMethods.push({ type: '', name: '' });
    },
    removeTeachingMethod(index) {
      this.formData.teachingMethods.splice(index, 1);
    },
    addAssessment() {
      this.formData.assessments.push({ name: '', weight: null });
    },
    removeAssessment(index) {
      this.formData.assessments.splice(index, 1);
    },
    addLiterature() {
      this.formData.literature.push({ name: '', type: '' });
    },
    removeLiterature(index) {
      if (this.formData.literature.length > 1) {
        this.formData.literature.splice(index, 1);
      }
    },
    addWorkload() {
      this.formData.workload.push({ type: '', hours: null });
    },
    removeWorkload(index) {
      this.formData.workload.splice(index, 1);
    },
    removeRequirement(index) {
      this.formData.requirementsList.splice(index, 1);
    },
    submitForm() {
      if (!this.canSubmit) {
        alert('Wypełnij poprawnie pola oraz upewnij się, że suma wag ocen wynosi 100%.');
        return;
      }
      // Tu można wysłać dane do API lub dalej przetwarzać
      alert('Sylabus został zapisany! (Symulacja)');
      console.log('Zapisane dane:', this.formData);
    },
    cancel() {
      if (confirm('Czy na pewno chcesz anulować i wyczyścić formularz?')) {
        this.resetForm();
      }
    },
    resetForm() {
      this.formData = {
        darkMode: this.formData.darkMode,
        name: '',
        code: '',
        unit: '',
        major: '',
        profile: '',
        specialty: '',
        moduleType: '',
        ects: null,
        yearSemester: '',
        coordinator: '',
        requirementsList: [''],
        goalsList: [''],
        outcomes: [],
        content: [],
        teachingMethods: [],
        assessments: [],
        literature: [],
        workload: [],
      };
    },
    print() {
      window.print();
    },
    addTeachingForm() {
      this.formData.teachingForms.push({ type: '', hours: '', mode: '' });
    },
    removeTeachingForm(index) {
      this.formData.teachingForms.splice(index, 1);
    },
    addEffect() {
      this.formData.effects.push({ number: '', method: '', gradeBelow50: '', grade50_59: '', grade60_74: '', grade75_100: '' });
    },
    removeEffect(index) {
      if (this.formData.effects.length > 1) {
        this.formData.effects.splice(index, 1);
      }
    }

  }
};
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Inter:wght@400;600&display=swap');

body {
  font-family: 'Inter', sans-serif;
  background-color: #f9f9fb;
  color: #333;
  font-size: 14px;
  line-height: 1.6;
  padding: 2rem;
  transition: background-color 0.3s ease, color 0.3s ease;
}

/* Nagłówki */
h4, h5 {
  color: #222;
  font-weight: 600;
  margin-bottom: 1rem;
  border-bottom: 2px solid #ddd;
  padding-bottom: 0.4rem;
  margin-top: 2.5rem;
}

/* Tabela */
.table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
  background-color: white;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 1px 3px rgba(0,0,0,0.05);
}

.table th,
.table td {
  border: 1px solid #e5e5e5;
  padding: 10px 12px;
  text-align: left;
  vertical-align: top;
}

.table th {
  background-color: #f1f3f6;
  font-weight: 600;
  color: #444;
}

/* Formularze */
.form-control,
.form-select {
  border: 1px solid #ccc;
  border-radius: 6px;
  padding: 8px 10px;
  font-size: 14px;
  width: 100%;
  background: white;
  color: #333;
  transition: border-color 0.2s ease;
}

.form-control:focus,
.form-select:focus {
  outline: none;
  border-color: #3b82f6;
  box-shadow: 0 0 0 2px rgba(59,130,246,0.15);
}

/* Przyciski */
button {
  font-size: 13px;
  padding: 6px 10px;
  border-radius: 6px;
  background-color: #3b82f6;
  color: white;
  border: none;
  transition: background-color 0.2s ease;
  cursor: pointer;
}

button:hover {
  background-color: #2563eb;
}

.btn-outline-primary {
  background-color: transparent;
  color: #3b82f6;
  border: 1px solid #3b82f6;
}

.btn-outline-primary:hover {
  background-color: #3b82f6;
  color: white;
}

.btn-danger {
  background-color: #ef4444;
}

.btn-danger:hover {
  background-color: #dc2626;
}

/* --- TRYB CIEMNY --- */
#app {
  background-color: #f9f9fb;
  color: #222222;
  min-height: 100vh;
  transition: background-color 0.3s ease, color 0.3s ease;
  font-family: 'Inter', sans-serif;
}

/* TRYB CIEMNY */
#app.dark-mode {
  background-color: #121318; /* bardzo ciemny, ale nie czarny */
  color: #e0e0e0;            /* delikatny jasny tekst */
}

/* Tabela */
#app.dark-mode .table {
  background-color: #1f2028; /* ciemnoszary granat */
  color: #e0e0e0;
  border-color: #3a3b47;
  box-shadow: 0 2px 8px rgba(0,0,0,0.5);
}

#app.dark-mode .table th {
  background-color: #292a37; /* trochę jaśniejsze od tła */
  color: #c0c0c0;
}

#app.dark-mode .table td {
  border-color: #3a3b47;
}

/* Formularze */
#app.dark-mode .form-control,
#app.dark-mode .form-select {
  background-color: #2a2b38;
  border: 1px solid #3a3b47;
  color: #e0e0e0;
}

#app.dark-mode .form-control::placeholder,
#app.dark-mode .form-select option {
  color: #888b99;
}

#app.dark-mode .form-control:focus,
#app.dark-mode .form-select:focus {
  border-color: #4f8ef7;
  box-shadow: 0 0 6px rgba(79, 142, 247, 0.6);
  outline: none;
}

/* Przycisk */
#app.dark-mode button {
  background-color: #4f8ef7;
  color: #f0f0f0;
  border: none;
  box-shadow: 0 3px 8px rgba(79, 142, 247, 0.5);
  transition: background-color 0.3s ease;
}

#app.dark-mode button:hover {
  background-color: #3c6ad8;
}

/* Bordery i linie */
#app.dark-mode h4, #app.dark-mode h5 {
  border-bottom: 2px solid #444758;
  color: #d0d0d0;
}
/* --- PRINT --- */

@media print {
  body {
    background: white !important;
    color: black !important;
    font-size: 12px !important;
    padding: 0 !important;
    margin: 0 !important;
  }

  .d-print-none {
    display: none !important;
  }

  input, select, button {
    border: none !important;
    background: none !important;
    color: black !important;
  }

  .table th,
  .table td {
    border: 1px solid #000 !important;
  }

  h4, h5 {
    border-bottom: 1px solid #000 !important;
    color: black !important;
  }
}
</style>