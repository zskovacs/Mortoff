import { ValidatorFn, FormControl, AbstractControl, ValidationErrors } from '@angular/forms';

export class FileValidator {

  static fileMaxSize(maxSize: number): ValidatorFn {
    const validatorFn = (file: File):any => {
      if (file instanceof File && file.size > maxSize) {
        return { fileMaxSize: { requiredSize: maxSize, actualSize: file.size, file } };
      }
    };
    return FileValidator.fileValidation(validatorFn);
  }

  static fileMinSize(minSize: number): ValidatorFn {
    const validatorFn = (file: File):any => {
      if (file instanceof File && file.size < minSize) {
        return { fileMinSize: { requiredSize: minSize, actualSize: file.size, file } };
      }
    };
    return FileValidator.fileValidation(validatorFn);
  }

  /**
   * extensions must not contain dot
   */
  static fileExtensions(allowedExtensions: Array<string>): ValidatorFn {
    const validatorFn = (file: File):any => {
      if (allowedExtensions.length === 0) {
        return null;
      }

      if (file instanceof File) {
        const ext = FileValidator.getExtension(file.name);
        if (allowedExtensions.indexOf(ext) === -1) {
          return { fileExtension: { allowedExtensions: allowedExtensions, actualExtension: file.type, file } };
        }
      }
    };
    return FileValidator.fileValidation(validatorFn);
  }

  private static getExtension(filename: string): string {
    if (filename.indexOf('.') === -1) {
      return '';
    }
    return filename.split('.').pop() as string;
  }

  private static fileValidation(validatorFn: (File: File) => null | object): ValidatorFn {
    return (formControl: AbstractControl): ValidationErrors | null => {
      if (!formControl.value) {
        return null;
      }

      const files: File[] = [];
      const isMultiple = Array.isArray(formControl.value);
      isMultiple
        ? formControl.value.forEach((file: File) => files.push(file))
        : files.push(formControl.value);

      for (const file of files) {
        return validatorFn(file);
      }

      return null;
    };
  }

}
