import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';

@Injectable()
export class AuthenticationService {

  constructor(private httpClient: HttpClient) {

  }
  authenticateUser(data) {
    // to check the user authentication
    return this.httpClient.post(`https://localhost:44348/auth/Login`, data,
    {
      headers: new HttpHeaders().set('Content-Type',"application/json")
    });
  }

  registeruser(data) {
    // to check the user authentication
    return this.httpClient.post(`https://localhost:44348/auth/Register`, data,
    {
      headers: new HttpHeaders().set('Content-Type',"application/json")
    });
  }

  setBearerToken(token,name) {
    // set the token to localstorage
    localStorage.setItem('bearerToken', token);
    localStorage.setItem('username', name);
  }

  setSocialBearerToken(username) {
    // set the token to localstorage
    localStorage.setItem('username', username);
    localStorage.setItem('isSocaialLogin', "true");
  }

  getBearerToken() {
    // get the  token from localstorage
    return localStorage.getItem('bearerToken');
  }

  getUserName(){
    return localStorage.getItem('username');
  }

  

  removeUserDetails(){
     localStorage.removeItem('bearerToken');
     localStorage.removeItem('username');
     localStorage.removeItem('isSocaialLogin')
  }

  // isUserAuthenticated(token,username): Promise<boolean> {
  //   return this.httpClient.post('https://localhost:5001/Authentiation/v1/isAuthenticated',
  //    {"usnername": name }, {
  //     headers: new HttpHeaders().set('Authorization', `Bearer ${token}`)
  //   })
  //   .map((res) => res['isAuthenticated'])
  //   .toPromise();
  // }
  
}
