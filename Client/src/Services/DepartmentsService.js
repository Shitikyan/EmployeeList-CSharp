import mockDepartments from "./Mock/department-data.json";
import { EMPLOYEE_MANAGER_API_HOST, IS_MOCK } from "../config";
import axios from "axios";

const getDepartmentsImpl = async () => {
  const response = await axios.get(`${EMPLOYEE_MANAGER_API_HOST}/department`);
  return response.data;
};

export const getDepartments = () =>
  IS_MOCK ? Promise.resolve(mockDepartments) : getDepartmentsImpl();
