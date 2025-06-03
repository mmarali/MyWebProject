<template>
  <div>
    <h2>Lista uczelni</h2>
    <table>
      <thead>
      <tr>
        <th>Nazwa</th>
        <th>Miasto</th>
        <th>Ulica</th>
        <th>Kod pocztowy</th>
        <th>Kraj</th>
      </tr>
      </thead>
      <tbody>
      <tr v-for="academy in academies" :key="academy.id">
        <td>{{ academy.name }}</td>
        <td>{{ academy.city }}</td>
        <td>{{ academy.street }}</td>
        <td>{{ academy.postcode }}</td>
        <td>{{ academy.country }}</td>
      </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  name: "AcademiesIndex",
  data() {
    return {
      academies: []
    };
  },
  mounted() {
    fetch('/api/academies')
        .then(res => {
          if (!res.ok) throw new Error('Brak dostępu');
          return res.json();
        })
        .then(data => {
          this.academies = data;
        })
        .catch(err => {
          console.error(err);
        });
  }
}
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 1rem;
}

th, td {
  padding: 8px 12px;
  border: 1px solid #ccc;
  text-align: left;
}

thead {
  background-color: #f5f5f5;
}
</style>
