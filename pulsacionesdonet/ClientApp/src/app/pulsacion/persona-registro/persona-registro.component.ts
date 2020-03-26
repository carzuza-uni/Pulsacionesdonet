import { Component, OnInit } from '@angular/core';
import { Persona } from '../models/persona';
import { PersonaService } from '../../services/persona.service';

@Component({
  selector: 'app-persona-registro',
  templateUrl: './persona-registro.component.html',
  styleUrls: ['./persona-registro.component.css']
})
export class PersonaRegistroComponent implements OnInit {
  persona: Persona;

  //validacion
  enviarDatos = false;
  errorIdentificacion = false;
  errorNombre = false;
  errorEdad = false;
  errorSexo = false;
  constructor(private personaService: PersonaService) { }

  ngOnInit() {
    this.persona = new Persona;
  }

  add(){    
    //$('#mensaje').addClass("hide");
    this.errorIdentificacion = false;
    this.errorNombre = false;
    this.errorEdad = false;
    this.errorSexo = false;
    this.enviarDatos = true;
		let validar = true;
		if(!this.persona.identificacion){
      validar = false;
      this.errorIdentificacion = true;
		}
		if(!this.persona.nombre){
      validar = false;
      this.errorNombre = true;
		}
		if(!this.persona.edad){
      validar = false;
      this.errorEdad = true;
		}
		if(!this.persona.sexo){
      validar = false;
      this.errorSexo = true;
		}
		if(!validar){
			return false;
    }
    let pulsacion = 0;
		if (this.persona.sexo == "F"){
      pulsacion = (210 - this.persona.edad) / 10;
    }else if (this.persona.sexo == "M"){
      pulsacion = (220 - this.persona.edad) / 10;
    }else{
      pulsacion = 0;
    }
    this.persona.pulsacion = pulsacion;
    this.personaService.post(this.persona);
    alert('Se agrego una nueva persona: '+ JSON.stringify(this.persona));
  }

}
