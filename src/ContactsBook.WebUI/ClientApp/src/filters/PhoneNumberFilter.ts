export default function (inputPhoneNumber: number):string {
    let i = 0;
    let phoneNumber: string = inputPhoneNumber.toString();
    phoneNumber = "+# (###) ### ## ##".replace(/#/g, (_) => phoneNumber[i++]);
    return phoneNumber;
  }