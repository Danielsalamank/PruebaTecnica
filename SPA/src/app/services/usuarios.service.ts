import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { Config } from 'src/environments/environment'
import { Router } from '@angular/router';
import { IUsuarios } from 'src/app/Interfaces/iusuarios';


@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  constructor(
              private http: HttpClient,
              private router: Router,
             ) { }


  CrearUsuario(Usuario: IUsuarios) : Observable<any>{
    // Se define el metodo del WS
    const path = `${Config.ServerApi}/api/Usuarios/RegistroUsuarios`;
    const vHeaders = new HttpHeaders();
    vHeaders.append('Accept', 'application/json');
    vHeaders.append('Content-Type', 'application/json' );
    vHeaders.append('Access-Control-Allow-Origin', '*' );
    vHeaders.append('Access-Control-Allow-Methods', 'POST, PUT, GET, OPTIONS, DELETE' );
    vHeaders.append('Access-Control-Allow-Headers', 'Content-Type, Authorization, Content-Length, X-Requested-With' );
    return this.http.post<any>(path, Usuario, {headers: vHeaders})
  }

  async ObtenerUsuarios() {
    // Se define el metodo del WS
    const path = `${Config.ServerApi}/api/Usuarios/Obtener`;
    const vHeaders = new HttpHeaders();
    vHeaders.append('Content-Type', 'application/json' );
    vHeaders.append('X-Requested-With', 'XMLHttpRequest' );
    vHeaders.append('Access-Control-Allow-Methods', 'POST, PUT, GET, OPTIONS, DELETE' );
    return this.http.get(path, {headers: vHeaders});
  }

  delay(ms: number) {
    return new Promise( resolve => setTimeout(resolve, ms) );
  }

  ActualizarUsuario(Usuario: IUsuarios) : Observable<any>{
    // Se define el metodo del WS
    const path = `${Config.ServerApi}/api/Usuarios/ActualizarUsuario`;
    const vHeaders = new HttpHeaders();
    vHeaders.append('Accept', 'application/json');
    vHeaders.append('Content-Type', 'application/json' );
    vHeaders.append('Access-Control-Allow-Origin', '*' );
    vHeaders.append('Access-Control-Allow-Methods', 'POST, PUT, GET, OPTIONS, DELETE' );
    vHeaders.append('Access-Control-Allow-Headers', 'Content-Type, Authorization, Content-Length, X-Requested-With' );
    return this.http.post<any>(path, Usuario, {headers: vHeaders})
  }


  async BorrarUsuarios(codigo: Number) {
    // Se define el metodo del WS
    const path = `${Config.ServerApi}/api/Usuarios/BorrarUsuario`;
    const vHeaders = new HttpHeaders();
    vHeaders.append('Content-Type', 'application/json' );
    vHeaders.append('X-Requested-With', 'XMLHttpRequest' );
    vHeaders.append('Access-Control-Allow-Methods', 'POST, PUT, GET, OPTIONS, DELETE' );
    const params = new HttpParams().set('CODIGO', codigo.toString());
    return this.http.delete(path, {params, headers: vHeaders});
  }

          
  
}
