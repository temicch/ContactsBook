export const emailRegex = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;

export const isCorrectEmail = (emailString: string): boolean => {
  emailString = emailString.trim();
  if (!emailString) return true;
  return emailRegex.test(emailString);
};

export const isCorrectPhoneNumber = (phone: string | number): boolean => {
  if (typeof phone === "string") phone = Number(phone);
  return phone <= 99999999999 && phone >= 10000000000;
};

export const binarySearch = function<T>(
  arr: Array<any>,
  item: T,
  comparer: (a: any, b: T) => number
): { isFounded: boolean; position: number } {
  if (arr === null || arr.length == 0) return { isFounded: false, position: 0 };

  let start = 0,
    end = arr.length - 1;

  while (start <= end) {
    let mid = Math.floor((start + end) / 2);

    if (comparer(arr[mid], item) == 0)
      return { isFounded: true, position: mid };
    else if (comparer(arr[mid], item) < 0) start = mid + 1;
    else end = mid - 1;
  }

  return { isFounded: false, position: start };
};

export const isContainsOnlyDigits = (str: string) => /^\d+$/.test(str.trim());
