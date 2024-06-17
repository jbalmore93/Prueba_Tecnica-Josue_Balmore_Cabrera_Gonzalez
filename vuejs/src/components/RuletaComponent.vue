<template>
  <div class="cuadro m-2 p-2 bg-success w-30 " >
    <label for="">Usuario: {{ nombre }}</label>
    <p for="form-label">Puntos restantes: {{ puntos= punto }}</p>
  </div>
    <div class="mt-3 p-3">
      <canvas class="p-5" id="canvas" width="500" height="500"></canvas>
    </div>
    <div class="cuadro " id="componente">
      <form-component :girar = "girar" :usuario = "usuario" :punto = "punto" 
      @actualizar="actualizar" @numeros = "numeros"/>
    </div>
  </template>
  
  <script>
import { ref } from 'vue';
import FormComponent from './FormComponent.vue';
import { useRoute } from 'vue-router';

  export default {
    data() {
      return {
        nombre : "",
        puntos: 0,
        opciones: Array.from({ length: 36 }, (_, i) => (i + 1).toString()),
        anguloInicial: 0,
        tiempoGiro: null,
        anguloGiroInicio: 0,
        tiempoActual: 0,
        tiempoTotalGiro: 0,
        numerar : [],
        ctx: null,
        resultado: null,
        leyenda: "",
        numero:0
      };
    },
    components:{
      'form-component' : FormComponent
    },
  created() {
    this.nombre = this.$route.params.nombre;
    this.puntos = this.$route.params.puntos;
    
  },
    computed: {
      arco() {
        return Math.PI / (this.opciones.length / 2);
      }
    },
    setup(){
      const route = useRoute();
      const usuario = ref(route.params.nombre);
      const punto = ref(route.params.puntos);

      function actualizar(data) {
       punto.value = data;
    }

    function numeros(data){
      this.numero.value = data;
      return this.numero;
    }

      return{
        usuario,
        punto,
        actualizar,
        numeros
      };
    },
    methods: {    
      
      dibujarRuleta() {
        var canvas = document.getElementById("canvas");
        if (canvas.getContext) {
          var radioExterior = 200;
          var radioTexto = 160;
          var radioInterior = 125;
  
          this.ctx = canvas.getContext("2d");
          this.ctx.clearRect(0, 0, 500, 500);
  
          this.ctx.strokeStyle = "black";
          this.ctx.lineWidth = 2;
  
          this.ctx.font = 'bold 12px Helvetica, Arial';
  
          for (var i = 0; i < this.opciones.length; i++) {
            var angulo = this.anguloInicial + i * this.arco;
            this.ctx.fillStyle = i % 2 === 0 ? "red" : "black";
  
            this.ctx.beginPath();
            this.ctx.arc(250, 250, radioExterior, angulo, angulo + this.arco, false);
            this.ctx.arc(250, 250, radioInterior, angulo + this.arco, angulo, true);
            this.ctx.stroke();
            this.ctx.fill();
  
            this.ctx.save();
            this.ctx.shadowOffsetX = -1;
            this.ctx.shadowOffsetY = -1;
            this.ctx.shadowBlur = 0;
            this.ctx.shadowColor = "rgb(220,220,220)";
            this.ctx.fillStyle = "white";
            this.ctx.translate(250 + Math.cos(angulo + this.arco / 2) * radioTexto, 
                               250 + Math.sin(angulo + this.arco / 2) * radioTexto);
            this.ctx.rotate(angulo + this.arco / 2 + Math.PI / 2);
            var texto = this.opciones[i];
            this.ctx.fillText(texto, -this.ctx.measureText(texto).width / 2, 0);
            this.ctx.restore();
          }
  
          // Flecha
          this.ctx.fillStyle = "black";
          this.ctx.beginPath();
          this.ctx.moveTo(250 - 4, 250 - (radioExterior + 5));
          this.ctx.lineTo(250 + 4, 250 - (radioExterior + 5));
          this.ctx.lineTo(250 + 4, 250 - (radioExterior - 5));
          this.ctx.lineTo(250 + 9, 250 - (radioExterior - 5));
          this.ctx.lineTo(250 + 0, 250 - (radioExterior - 13));
          this.ctx.lineTo(250 - 9, 250 - (radioExterior - 5));
          this.ctx.lineTo(250 - 4, 250 - (radioExterior - 5));
          this.ctx.lineTo(250 - 4, 250 - (radioExterior + 5));
          this.ctx.fill();
        }
      },
      girar() {
        this.anguloGiroInicio = Math.random() * 10 + 10;
        this.tiempoActual = 0;
        this.tiempoTotalGiro = Math.random() * 3 + 4 * 200;
        this.rotarRuleta();
      },
      rotarRuleta() {
        this.tiempoActual += 30;
        if (this.tiempoActual >= this.tiempoTotalGiro) {
          this.detenerRuleta();
          return;
        }
        var anguloGiro = this.anguloGiroInicio - this.desacelerar(this.tiempoActual, 0, this.anguloGiroInicio, this.tiempoTotalGiro);
        this.anguloInicial += (anguloGiro * Math.PI / 180);
        this.dibujarRuleta();
        this.tiempoGiro = setTimeout(() => this.rotarRuleta(), 30);
      },
      detenerRuleta() {
        clearTimeout(this.tiempoGiro);
        var grados = this.anguloInicial * 180 / Math.PI + 90;
        var arcd = this.arco * 180 / Math.PI;
        var indice = Math.floor((360 - grados % 360) / arcd);
        this.resultado = this.opciones[indice];
        this.ctx.save();
        this.ctx.font = 'bold 30px Helvetica, Arial';
        //var texto = this.resultado;
        //this.resultado = this.numerar.numero;
        var numero = this.numeros();
        this.ctx.fillText(numero, 250 - this.ctx.measureText(numero).width / 2, 250 + 10);
        this.ctx.restore();
  
        // Verificar si el resultado es par o impar y manejar puntos de apuesta
        if (this.resultado % 2 === 0) {
          console.log(`El número ${this.resultado} es par`);
          this.leyenda = `El número ${this.numerar.numero} es par y el color es negro`;
          // Aquí puedes añadir la lógica para manejar puntos de apuesta cuando es par
        } else {
          console.log(`El número ${this.resultado} es impar`);
          this.leyenda = `El número ${this.resultado} es impar y el color es rojo`;
        }
      },
      desacelerar(t, b, c, d) {
        var ts = (t /= d) * t;
        var tc = ts * t;
        return b + c * (tc + -3 * ts + 3 * t);
      }
    },
    mounted() {
      this.dibujarRuleta();
    }
  };
  </script>
  
  <style scoped>
  #spin {
    margin-bottom: 20px;
  }

  .numero-fixed {
  position: absolute;
  bottom: -40px; /* Ajusta la posición vertical dentro de la ruleta según sea necesario */
  left: 50%; /* Ajusta la posición horizontal dentro de la ruleta */
  transform: translateX(-50%);
}
.cuadro{
  width: 50%;
  position: absolute;
  left: 25%;
}
#componente{
  position: absolute;
  top: 65%;
}
  </style>