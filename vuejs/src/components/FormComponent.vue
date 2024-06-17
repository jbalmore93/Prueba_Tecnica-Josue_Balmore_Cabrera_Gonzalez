<!-- eslint-disable vue/valid-template-root -->
<template>
  <div class="bg-info opacity-65 text-white border-radius">
    <select v-model="tipo" class="bg-secondary mt-2">
      <option class="text-info" disabled>---Tipo Apuesta---</option>
      <option class="text-info" value="1">Apostar por color</option>
      <option class="text-info" value="2">Apostar por Par e Impar</option>
      <option class="text-info" value="3">Apostar por Numero y Color</option>
    </select>
    <div class="bg-info text-white">
      <v-responsive
    class="mx-auto mt-2"
    max-width="300"
    max-height="100"
  >
      <v-text-field
        type="number"
        class="mx-2 bg-info"
        :rules="[rules.required]"
        v-model="apuesta"
        placeholder="500"
        label="Insertar apuesta"
        :disabled="tipo==='' "
      ></v-text-field>
      </v-responsive>
    </div>
    <div v-if="parseFloat(apuesta) > parseFloat(punto)">
      <strong for="" class="text-danger">No tienes suficientes puntos</strong>
    </div>
    <div v-if="parseFloat(apuesta) <= 0 || apuesta == ''">
      <strong for="" class="text-danger">Ingrese un valor valido</strong>
    </div>
    <div>
      <div
        class="bg-info text-white mt-2"  
        v-if="tipo == '1' || tipo == '3'"
      >
        <label for="">Insertar Color</label>
        <select name="color" id="color" class="mx-2 bg-success" v-model="color">
          <option value="" disabled>---Elija un color---</option>
          <option value="rojo">Rojo</option>
          <option value="negro">Negro</option>
        </select>
      </div>
      <div class="bg-info text-white"  v-if="tipo == '2'">
        <label for="">Par e Impar</label>
        <select name="pare" id="pare" class="mx-2 bg-success" v-model="pare">
          <option value="" disabled>---Elija la opcion---</option>
          <option value="par">Numero Par</option>
          <option value="impar">Numero Impar</option>
        </select>
      </div>
      <div class="bg-info text-white mt-2" v-if="tipo == '3'">
        <label for="">Insertar numero de la suerte</label>
        <input
          type="number"
          placeholder="Numero de la suerte"
          class="mx-2 bg-success"
          v-model="num"
          name=""
          id=""
        />
      </div>
      <div v-if="parseInt(num) > 36 || parseInt(num) < 0">
        <strong for="" class="text-danger">Numero fuera del rango 1-36</strong>
      </div>
      <v-btn
        size="small"
        @click="jugar()"
        class="btn btn-sm btn-success"
        v-if="mostrar == false"
        :disabled="
          apuesta.length === 0 ||
          parseInt(apuesta) > parseFloat(punto) ||
          parseInt(apuesta) <= 0 ||
          ((tipo === '1' || tipo === '3') &&
            !(color === 'rojo' || color === 'negro')) ||
          tipo === '' ||
          (tipo === '2' && !(pare === 'par' || pare === 'impar')) ||
          (tipo === '3' &&
            (parseInt(num) > 36 || parseInt(num) < 1 || num === ''))
        "
      >
        Jugar
      </v-btn>
      <v-btn size="small" class="m-2" color="primary" text>
        <router-link to="/" class="text-white no-underline">Salir</router-link>
      </v-btn>
    </div>
    <div class="ventana opacity-65" v-show="mostrar">
      <div class="mt-2">
        <label for="" class="mt-2 text-white">Numero : {{ numero }}</label>
      </div>
      <div class="mt-2">
        <label for="" class="mt-2 text-white">Color : {{ colores }}</label>
      </div>
      <div class="mt-2">
        <label for="" class="mt-2 text-white">El numero es : {{ pares }}</label>
      </div>
      <div class="mt-2">
        <label for="" class="mt-2 text-white">
          Resultado: {{ resultado }}</label
        >
      </div>
      <div class="mt-2">
        <label for="" class="mt-2 text-white"> Puntos: {{ restante }}</label>
      </div>
      <div class="mt-2">
        <v-btn color="success" @click="cerrar()"> Jugar otra vez </v-btn>
        <v-btn class="mt-2 btn btn-info" @click="guardar()">
          Guardar y Salir
        </v-btn>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import { defineComponent } from "vue";

export default defineComponent({
  name: "FormComponent",
  props: {
    usuario: String,
    punto: String,
    girar: Function,
  },
  emits: ["actualizar", "numeros"],
  setup(props, { emit }) {
    function actualizar() {
      emit("actualizar", this.restante);
    }

    function numeros() {
      const dato = this.numero;
      emit("numeros", dato);
    }

    function callgirar() {
      if (props.girar) {
        props.girar();
      }
    }
    return {
      actualizar,
      callgirar,
      numeros,
    };
  },
  data() {
    return {
      rules:[
      {nombre: 'apuesta' ,required : value => !!value ||'El campo es requerido'},
      ],
      nombre: this.usuario,
      puntos: this.punto,
      number: "",
      color: "ninguno",
      num: 0,
      mostrar: false,
      pare: "ninguno",
      tipo: "",
      numero: 0,
      colores: "",
      pares: "",
      resultado: 0,
      restante: 0,
      apuesta: 0,
    };
  },
  methods: {
    cerrar() {
      this.mostrar = false;
      this.num = 0;
      this.apuesta = 0;
      this.color = "ninguno";
      this.pare = "ninguno";
      this.tipo = "";
      this.actualizar();
    },
    guardar() {
      axios
        .post("http://localhost:5078/api/usuario/", {
          nombre_usuario: this.nombre,
          suma_puntos: this.restante,
        })
        .catch(function (error) {
          console.log(error);
        });
      this.mostrar = false;
      this.$router.push("/");
    },
    jugar() {
      this.mostrar = true;
      this.callgirar();
      axios
        .post("http://localhost:5078/api/usuario/juego", {
          numero: this.num,
          color: this.color,
          par: this.pare,
          apuesta: this.apuesta,
          puntos: this.punto,
        })
        .then((response) => {
          this.numerar = response.data;
          this.numero = this.numerar.numero;
          this.colores = this.numerar.colores;
          this.pares = this.numerar.pares;
          this.resultado = this.numerar.resultado;
          this.restante = this.numerar.restante;
        })
        .catch((error) => {
          console.error("Error al obtener los datos:", error);
        });
    },
  },
});
</script>

<style scoped>
.ventana {
  background-color: orange;
  width: 250px;
  height: auto;
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
}
.no-underline {
  text-decoration: none !important;
}
</style>
