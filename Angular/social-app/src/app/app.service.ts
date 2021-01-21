import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppService {

  constructor(private http: HttpClient) { }
  baseUrl: string = 'http://localhost:54859/';
  register(userdata) {
    return this.http.post(this.baseUrl + 'AddUser', userdata);
  };
  login(userdata) {
    return this.http.post(this.baseUrl + 'Login', userdata);
  };
  SocialRegister(userdata) {
    return this.http.post(this.baseUrl + 'SocialLogin', userdata);
  }
}
