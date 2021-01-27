import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AppService } from 'src/app/app.service';

@Component({
  selector: 'app-dash-home',
  templateUrl: './dash-home.component.html',
  styleUrls: ['./dash-home.component.css']
})
export class DashHomeComponent implements OnInit {
  entityData: any;
  entityForm: FormGroup;
  updatedEntity: Array<any> = [];
  isShowForm : boolean = false;
  constructor(private appService: AppService, private router: Router, private fb: FormBuilder) {}

  ngOnInit() {
    this.createForm();
    this.GetAllEntity();
  }
  createForm() {
    this.entityForm = this.fb.group({
      entityType: [null],
      entityName: [null]
    })
  }
  get formControls() { return this.entityForm.controls; }
  GetAllEntity() {
    this.appService.GetAllEntity().subscribe((data: any) => {
      if (data.statusCode == 200) {
        this.entityData = data.responseData;
      } else {
        window.alert(data.message);
      }

    });
  }
  changeVal(i,event){
    let entData = this.entityData[i];
    let newEnt = {
      entityId: entData.entityId,
      isChecked: event.target.checked
    }
    let isExist = this.updatedEntity.findIndex(x=>x.entityId == newEnt.entityId);
    if(isExist>=0) {
      this.updatedEntity.splice(isExist,1);
    }
    this.updatedEntity.push(newEnt);
  }
  update(){
    this.appService.UpdateEntities(this.updatedEntity).subscribe((data:any)=>{
      if(data.statusCode == 200) {
        this.entityData = data.responseData;
        window.alert(data.message);
      } else {
        window.alert(data.message);
      }
    })
  }
  showForm() {
    this.isShowForm = !this.isShowForm;
  }
  addEntity(){
    if (!this.entityForm.errors) {
      let data = this.entityForm.value;
        this.appService.AddEntities(data).subscribe((data: any) => {
          if (data.statusCode == 200) {
            window.alert(data.message);
            this.GetAllEntity();
          } else {
            window.alert(data.message);
          }
  
        });
      } else {
        window.alert("Please fill all fields!");
      }
  }
}
