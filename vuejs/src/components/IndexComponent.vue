<template>
  <div class="contenedor bg-info opacity-75" v-show="inicio">
    <div class="text-center">
      <v-btn class="btn btn-secondary" @click="lanzar()">Iniciar Juego</v-btn>
    </div>
    <div class="mt-3 text-center">
      <v-btn class="btn btn-secondary" @click="load()">Cargar Juego</v-btn>
    </div>
  </div>
  <div class="contenedor bg-info opacity-75" v-show="jugar">
    <div class="text-center center">
      <input
        type="text"
        class="bg-black text-white text-center"
        placeholder="Usuario"
        v-model="usuario"
        required
      />
    </div>
    <div v-if="!usuario.length" class="text-center">
      <strong for="" class="text-danger">Ingresar nombre de usuario</strong>
    </div>
    <strong for="" class="text-danger">{{ mensaje }}</strong>
    <div v-if="empezar==true">
    <div class="mx-0 mt-3 text-center center">
      <input
        type="number"
        v-model="restantes"
        required
        placeholder="Puntos"
        class="bg-black text-white text-center"
      />
    </div>
    <div
      v-if="
        restantes === null ||
        restantes === undefined ||
        restantes === '' ||
        restantes <= 0
      "
      class="text-center"
    >
    </div>
      <strong class="text-danger">Ingresar puntos</strong>
    </div>

    <div class="mt-3 text-center">
      <v-btn @click="iniciar()" class="btn btn-secondary">Atras</v-btn>
      <div v-if="cargar==true">
      <v-btn color="secondary" @click="obtener()" class="m-2" :disabled="usuario.length===0">
       Cargar</v-btn></div>
      <div v-if="empezar==true">
      <v-btn color="secondary" class="m-2" :disabled="restantes<=0 && usuario.length===0">
        <router-link style="text-decoration: none !important; "
        :to="usuario ? '/Ruleta/' + usuario + '/' + restantes : ''"
        >Empezar</router-link
      >
      </v-btn>
    </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  data() {
    return {
      productos: [],
      inicio: true,
      restantes: "",
      juego: false,
      usuario: "",
      empezar:false,
      cargar:false,
      mensaje:""
    };
  },
  methods: {
    obtener() {
      console.log(this.usuario);
      axios
        .get("http://localhost:5078/api/usuario/"+this.usuario)
        .then((response) => {
          this.usuario = response.data[0].nombre_usuario;
          this.restantes = response.data[0].suma_puntos;
          console.log(this.usuario);
          console.log(this.restantes);
          console.log(response.data);
          if(this.usuario!="" && this.restantes != ""){
          this.$router.push('/Ruleta/'+this.usuario+'/'+this.restantes); 
        }
        })
        .catch((error) => {
          this.mensaje = "Usuario no existe";
          console.error(error);
        });
    },
    load(){
      this.cargar = true;
      this.jugar = true;
      this.inicio = false;
    },
    lanzar() {
      this.empezar = true;
      this.jugar = true;
      this.inicio = false;
    },
    iniciar() {
      this.jugar = !this.jugar;
      this.inicio = !this.inicio;
      this.cargar = false;
      this.empezar = false;
      this.usuario = "";
      this.restantes = "";
      this.mensaje = "";
    },
  },
};
</script>

