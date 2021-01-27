import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService, FacebookLoginProvider, GoogleLoginProvider, } from 'angular-6-social-login';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginform: FormGroup;
  formdata: any;
  SocialLoginData: any;
  Provider: string;
  SocialEmail: string;
  submitted: boolean = false;
  constructor(private fb: FormBuilder, private appService: AppService, private router: Router, private authService: AuthService) { }

  ngOnInit() {
    this.createForm();
  }
  get formControls() { return this.loginform.controls; }
  createForm() {
    this.loginform = this.fb.group({
      email: [null, Validators.compose([Validators.required, Validators.pattern(/^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/)])],
      password: [null, Validators.required]
    });
  };
  login() {
    if (this.loginform.valid) {
      let data = {
        Email: this.loginform.value.email,
        Password: this.loginform.value.password,
      };
      this.appService.login(data).subscribe((data: any) => {
        if (data.statusCode == 200) {
          window.alert(data.message);
          this.router.navigate(['/login']);
        } else {
          window.alert(data.message);
        }
      });
    } else {
      window.alert("Please fill all fields!");
    }
  }

  socialsigin(platform: string) {
    let socialPlatformProvider;
    if (platform == "facebook") {
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    }
    else if (platform == "gmail") {
      socialPlatformProvider = GoogleLoginProvider.PROVIDER_ID;
    }
    this.authService.signIn(socialPlatformProvider).then((userdata) => {
      console.info(userdata);
      this.appService.SocialRegister(userdata).subscribe((data: any) => {
        if (data.statusCode == 200) {
          window.alert(data.message);
        } else {
          window.alert(data.message);
        }
      })
    });
  }

}
