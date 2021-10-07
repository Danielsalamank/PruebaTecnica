import { Component, OnInit, ViewChild } from '@angular/core';
import { UsuariosService } from 'src/app/services/usuarios.service';
import { MatTable } from '@angular/material/table';
import { IUsuarios } from 'src/app/Interfaces/iusuarios';


@Component({
  selector: 'app-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.scss']
})
export class UsuariosComponent implements OnInit {

  formDatos: IUsuarios = {
    codigo: 0,
    nombres: '',
    apellido: '',
    fechA_NACIMIENTO: new Date(),
    fotO_USUARIO: '',
    estadO_CIVIL: '',
    tienE_HERMANOS: true
  };
  formDatosActivo = false;
  optinsert = false;
  

  constructor(private ServUsuario: UsuariosService) { }

  ngOnInit(): void {
    this.ObtenerUsuarios();
  }

  ListUsuario:any = [];
  totalUsuarios: any;

 

  async ObtenerUsuarios(){
    await this.ServUsuario.ObtenerUsuarios()
    .then(response => {
      console.log(response);
      response.subscribe(resp => {
        console.log(resp);
        this.ListUsuario =  resp;
        this.totalUsuarios = 0;
        Object.values(resp).forEach(element => {
          this.totalUsuarios = this.totalUsuarios + element.VALOR_Usuario;
        });
        // se realiza la sumatoria de todas las Usuarios
      });
    })
    .catch(error => {
      console.log(error);
    });
  }
  
  ActivarActualizacion(item: IUsuarios) {
    this.optinsert = true;
    console.log(item);
    console.log(item.codigo);
    this.formDatos.codigo = Number(item.codigo);
    this.formDatos.nombres = item.nombres;
    this.formDatos.apellido = item.apellido;
    this.formDatos.fechA_NACIMIENTO = item.fechA_NACIMIENTO;
    // this.formDatos.fotO_USUARIO = item.fotO_USUARIO;
    this.formDatos.estadO_CIVIL = item.estadO_CIVIL;
    this.formDatos.tienE_HERMANOS = item.tienE_HERMANOS;
    this.formDatosActivo = true;
  }

  

  ActualizarUsuario() {
    if (confirm("Esta seguro de modificar este usuario?")) {
      this.ServUsuario.ActualizarUsuario(this.formDatos).subscribe(response => {
        this.ObtenerUsuarios();
        this.LimpiarFormulario();
        alert('Datos actualizados exitosamente');
      });
    }
  }

  ActivarInsercion() {
    this.optinsert = true;
    this.LimpiarFormulario();
    this.formDatosActivo = true;
  }

  InsertarDatos() {
    if (confirm("Esta seguro de insertar este usuario?")) {
      this.ServUsuario.CrearUsuario(this.formDatos).subscribe(response => {
        this.LimpiarFormulario();
        this.ObtenerUsuarios();
        alert('Datos Insertados exitosamente');
      });
    }    
  }

  LimpiarFormulario() {
    this.formDatos.codigo = 0;
    this.formDatos.nombres = '';
    this.formDatos.apellido = '';
    this.formDatos.fechA_NACIMIENTO = new Date();
    this.formDatos.fotO_USUARIO = '';
    this.formDatos.estadO_CIVIL = '';
    this.formDatos.tienE_HERMANOS = true;
    this.formDatosActivo = false;
    this.optinsert = false;
  }


  handleUpload(event: any) {
    const file = event.target.files[0];
    const reader = new FileReader();
    
    reader.readAsDataURL(file);

    reader.onload = (ret) => {
      // this.formDatos.fotO_USUARIO = btoa(reader.result.);
      ret.target?.result
      
        console.log(reader.result);
        this.formDatos.fotO_USUARIO = (reader.result as string);
        // this.formDatos.fotO_USUARIO = this.formDatos.fotO_USUARIO.split()
    };
  }


  async BorrarUsuario(codigo: number) {
    if (confirm("Realmente quiere borrar este usuario?" + codigo)) {
      await this.ServUsuario.BorrarUsuarios(codigo)
      .then(response => {
        console.log(response);
        response.subscribe(resp => {
          this.ObtenerUsuarios();
          this.LimpiarFormulario();
          alert('Dato Borrado exitosamente');
        });
      })
      .catch(error => {
        console.log(error);
      });
    }
  }



}
