import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-dynamic-form',
  templateUrl: './dynamic-form.component.html',
  styleUrls: ['./dynamic-form.component.css']
})
export class DynamicFormComponent implements OnInit {
  registerForm: FormGroup;
  entities: Array<any> = [];
  entityData: any;
  newData: Array<any> = [];

  constructor(private fb: FormBuilder, private appService: AppService, private router: Router) {
  }
  ngOnInit() {
    this.getActiveEntities();
  }
  getActiveEntities() {
    this.appService.GetActiveEntity().subscribe((data: any) => {
      if (data.statusCode == 200) {
        this.entityData = data.responseData;
        this.entityData.forEach(x => {
          this.entities.push(x.entityName);
        });
        this.createForm(this.entities);
      } else {
        window.alert(data.message);
      }

    });
  }
  createForm(data) {
    let group = {}
    data.forEach(x => {
      group[x] = new FormControl('');
    })
    this.registerForm = new FormGroup(group);
  }
  get formControls() { return this.registerForm.controls; }
  register() {
    if (!this.registerForm.errors) {
      let data = this.registerForm.value;
      Object.keys(data).forEach(key => {
        let modifiedData = {
          entityName: key,
          entityType: data[key]
        }
        this.newData.push(modifiedData);
      });
      this.appService.AddEntities(this.newData).subscribe((data: any) => {
        if (data.statusCode == 200) {
          window.alert(data.message);
        } else {
          window.alert(data.message);
        }

      });
    } else {
      window.alert("Please fill all fields!");
    }
  }

}
