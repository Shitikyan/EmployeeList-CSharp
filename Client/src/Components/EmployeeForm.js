import { Modal, Form, Input, DatePicker, Select } from "antd";
import dayjs from "dayjs";
import { useEffect, useState } from "react";
import { getDepartments } from "../Services/DepartmentsService";

export const editState = "edit";
export const addState = "open";

export const EmployeeForm = ({ open, state, employee, onCommit, onCancel }) => {
  const [form] = Form.useForm();
  const [departments, setDepartments] = useState([]);

  useEffect(async () => {
    const departments = await getDepartments();
    setDepartments(departments);
  }, []);

  useEffect(() => {
    form.setFieldsValue(
      {
        name: employee.name,
        email: employee.email,
        dateOfBirth: employee.dateOfBirth
          ? dayjs(employee.dateOfBirth, "YYYY-MM-DD")
          : undefined,
        department: employee.departmentId,
      },
      [employee]
    );
  });

  return (
    <Modal
      layout="vertical"
      title={state === addState ? "Add a new employee" : "Edit an existing employee"}
      open={open}
      onOk={async () => {
        const formValues = await form.validateFields();
        employee.name = formValues.name;
        employee.email = formValues.email;
        employee.dateOfBirth = formValues.dateOfBirth.format("YYYY-MM-DD");
        employee.departmentId = formValues.department;
        const matchingDepartment = departments.find(
          (dept) => dept.id === formValues.department
        );
        employee.departmentName = matchingDepartment.name;
        onCommit();
      }}
      onCancel={() => {
        onCancel();
      }}
    >
      <Form
        form={form}
        name="employeeForm"
        labelCol={{ span: 8 }}
        wrapperCol={{ span: 16 }}
        style={{ maxWidth: 600 }}
      >
        <Form.Item
          label="Name"
          name="name"
          rules={[{ required: true, message: "Please input the empoyee name!" }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Email"
          name="email"
          rules={[
            {
              required: true,
              type: "email",
              message: "Please input a valid email!",
            },
          ]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Date of Birth"
          name="dateOfBirth"
          rules={[
            { required: true, message: "Please select the empoyee date of birth!" },
          ]}
        >
          <DatePicker />
        </Form.Item>
        <Form.Item
          label="Department"
          name="department"
          rules={[
            { required: true, message: "Please select the employee department!" },
          ]}
        >
          <Select
            style={{ width: 120 }}
            placeholder="Select the employee's department"
            options={departments.map((dept) => ({
              value: dept.id,
              label: dept.name,
            }))}
          ></Select>
        </Form.Item>
      </Form>
    </Modal>
  );
};
