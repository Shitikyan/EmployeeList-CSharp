import axios from "axios";
import { EMPLOYEE_MANAGER_API_HOST, IS_MOCK } from "../config";
import mockEmployees from "./Mock/data.json";

const getEmployeesImpl = async (searchTerm) => {
  const querySegment = `?searchTerm=${searchTerm}`;
  const response = await axios.get(
    `${EMPLOYEE_MANAGER_API_HOST}/employee${searchTerm ? querySegment : ""}`
  );
  return response.data;
};
export const getEmployees = (searchTerm) =>
  IS_MOCK ? Promise.resolve(mockEmployees) : getEmployeesImpl(searchTerm);

export const createEmployee = async (employee) => {
  await axios.post(`${EMPLOYEE_MANAGER_API_HOST}/employee`, employee);
};

export const updateEmployee = async (employee) => {
  await axios.put(`${EMPLOYEE_MANAGER_API_HOST}/employee/${employee.id}`, employee);
};

export const deleteEmployee = async (employeeId) => {
  await axios.delete(`${EMPLOYEE_MANAGER_API_HOST}/employee/${employeeId}`);
};
