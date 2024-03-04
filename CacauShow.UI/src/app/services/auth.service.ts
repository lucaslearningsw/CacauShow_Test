import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http"
import { LoginUser } from '../Components/models/loginUser.model';
import { environment } from 'src/environments/environment';
import { userRegister } from '../Components/models/userRegister';
import { User } from '../Components/models/user';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  baseApiUrl: string = environment.baseApiUrl;
  constructor(private http : HttpClient) { }


  getAllUsers():Observable<User[]>
  {
   return this.http.get<User[]>(`${this.baseApiUrl}User/lista-usuarios`)
  }

  signUp(userObj:any)
  {   
     return this.http.get<any>(`${this.baseApiUrl}User/signUp`, userObj)

  }

  Login(loginUser:LoginUser)
  {
    return this.http.post<any>(`${this.baseApiUrl}User/login`, loginUser)
  }

  RegisterUser(userObj:userRegister)
  {
    console.log(userObj);
    return this.http.post<any>(`${this.baseApiUrl}User/registrar-usuario`, userObj)
  }

  getUserById(id:string): Observable<User>{
   return this.http.get<User>(`${this.baseApiUrl}User/detalhe-usuario/` + id)
  }

  updateUser(id:string, updateUser: User): Observable<User>
  {
    return this.http.post<User>(`${this.baseApiUrl}User/atualizar-usuario/` + id,updateUser)
  }

  deleteUser(id:string): Observable<User>
  {
    return this.http.delete<User>(`${this.baseApiUrl}User/excluir-usuario/` + id)
  }
}
