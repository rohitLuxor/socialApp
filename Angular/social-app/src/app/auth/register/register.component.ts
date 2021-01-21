import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  constructor(private fb: FormBuilder, private appService: AppService, private router: Router) {
  }
  ngOnInit() {
    this.createForm();
  }
  createForm() {
    this.registerForm = this.fb.group({
      UserName: [null],
      FirstName: [null],
      LastName: [null],
      Email: [null],
      Password: [null],
    });
  }
  get formControls() { return this.registerForm.controls; }
  register() {
    if (this.registerForm.valid) {
      let data = {
        UserName: this.registerForm.value.UserName,
        FirstName: this.registerForm.value.FirstName,
        LastName: this.registerForm.value.LastName,
        Email: this.registerForm.value.Email,
        Password: this.registerForm.value.Password,
      };
      this.appService.register(data).subscribe((data: any) => {
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
}
