export interface ICustomer{
  id: number;
  name: string;
  email: string;
  phoneNumber: string;
  gender: string;
  password: string;
  accessToken: string;
  accessExpries: string;
  refreshToken: string;
  refreshExpries: string;
  address: IAddress[]
}

export interface IAddress{
  street: string;
  address1: string;
  address2: string;
  address3: string;
}