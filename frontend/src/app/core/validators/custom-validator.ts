import {AbstractControl, ValidatorFn} from "@angular/forms";

export class CustomValidator {
  static numberRange(prms: any = {}): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      let val: number = control.value;
      if (isNaN(val) || /\D/.test(val.toString())) {
        // Is not a number
        return {"number": true};
      } else if (!isNaN(prms.min) && !isNaN(prms.max)) {
        // value below 'min' or above 'max'
        return val < prms.min || val > prms.max ? {"number": true} : null;
      } else if (!isNaN(prms.min)) {
        // value below 'min'
        return val < prms.min ? {"number": true} : null;
      } else if (!isNaN(prms.max)) {
        // value above 'max'
        return val > prms.max ? {"number": true} : null;
      }
      return null;
    };
  }
}
