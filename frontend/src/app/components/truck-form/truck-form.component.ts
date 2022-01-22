import { Component, OnInit } from '@angular/core';
import {AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RxwebValidators } from '@rxweb/reactive-form-validators';
import { first } from 'rxjs';
import { AlertService } from 'src/app/services/alert.service';
import { TruckService } from 'src/app/services/truck.service';
import {CustomValidator} from "../../core/validators/custom-validator";
import Swal from "sweetalert2";

@Component({
  selector: 'app-truck-form',
  templateUrl: './truck-form.component.html',
  styleUrls: ['./truck-form.component.css']
})
export class TruckFormComponent implements OnInit {
  isAddMode!: boolean;
  form!: FormGroup;
  id!: string;
  loading = false;
  submitted = false

  currentYear: number = new Date().getFullYear();
  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private truckService: TruckService,
    private alertService: AlertService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;

    this.form = this.formBuilder.group({
      id: ['', [Validators.nullValidator]],
      model: ['', [Validators.required]],
      color: ['', [Validators.required]],
      manufactureYear: ['', [Validators.required, RxwebValidators.digit,
        CustomValidator.numberRange({min: this.currentYear, max: this.currentYear})]
      ],
      modelYear: ['', [Validators.required, RxwebValidators.digit,
        CustomValidator.numberRange({min: this.currentYear, max: this.currentYear+1})]
      ]
    });

    if (this.isAddMode){
      this.form.controls['manufactureYear'].setValue(this.currentYear);
      this.form.controls['manufactureYear'].disable();
    } else {
      this.truckService.getById(this.id)
        .pipe(first())
        .subscribe(x => this.form.patchValue(x));
    }
  }

  onSubmit(): void {
    this.submitted = true;
    this.alertService.clear();

    if (this.form.invalid){
      return;
    }

    this.loading = true;
    if (this.isAddMode) {
      this.createTruck();
    } else {
      this.updateTruck();
    }
  }

  private createTruck(): void {
    let formValue = this.form.value;
    formValue['manufactureYear'] = this.currentYear;
    delete formValue['id'];
    this.truckService.create(formValue)
      .pipe(first())
      .subscribe(
        () => this.showSuccess('Truck added successfully'),
        () => this.showError()
      ).add(() => this.loading = false);
  }

  private updateTruck(): void {
    //let formValue = this.form.getRawValue();
    this.truckService.update(this.id, this.form.value)
      .pipe(first())
      .subscribe(
        () => this.showSuccess('Truck updated successfully'),
        () => this.showError()
      ).add(() => this.loading = false);
  }

  showSuccess(msg: string) : void {
    Swal.fire('Success', msg, 'success')
      .then(() => this.router.navigate(['/trucks']));
  }
  showError() : void {
    Swal.fire('Error', 'An error happened during operation', 'error');
  }
}
