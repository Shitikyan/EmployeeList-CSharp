import React, { useState, useEffect } from "react";
import { Table, Button, Space, Input } from "antd";
import { EmployeeForm, addState, editState } from "./EmployeeForm";

import {
  createEmployee,
  deleteEmployee,
  getEmployees,
  updateEmployee,
} from "../Services/EmployeesService";

const { Search } = Input;

const EmployeeList = () => {
  const [employees, setEmployees] = useState([]);

  const [open, setOpen] = useState(false);
  const [modalState, setModalState] = useState(addState);
  const [employee, setEmployee] = useState({});

  const onEmployeeAddRequested = () => {
    setEmployee({});
    setModalState(addState);
    setOpen(true);
  };

  const onEmployeeRemoved = async (employee) => {
    await deleteEmployee(employee.id);
    setEmployees(await getEmployees());
  };

  const onEmpoyeeEditRequested = (e) => {
    setEmployee(e);
    setModalState(editState);
    setOpen(true);
  };

  const onEmployeeUpdated = async () => {
    if (modalState === addState) {
      await createEmployee(employee);
      setEmployees(await getEmployees());
    } else {
      await updateEmployee(employee);
      setEmployees(await getEmployees());
    }

    setOpen(false);
  };

  const onSearch = async (value) => {
    setEmployees(await getEmployees(value));
  };

  useEffect(async () => {
    const employees = await getEmployees();
    setEmployees(employees);
  }, []);

  const columns = [
    {
      title: "Name",
      dataIndex: "name",
    },
    {
      title: "Email",
      dataIndex: "email",
    },
    {
      title: "Date of Birth",
      dataIndex: "dateOfBirth",
    },
    {
      title: "Department",
      dataIndex: "departmentName",
    },
    {
      title: "Actions",
      key: "actions",
      render: (_, e) => {
        return (
          <Space size="middle">
            <Button type="primary" onClick={() => onEmpoyeeEditRequested(e)}>
              Edit
            </Button>
            <Button type="primary" onClick={() => onEmployeeRemoved(e)}>
              Delete
            </Button>
          </Space>
        );
      },
    },
  ];

  return (
    <div>
      <EmployeeForm
        open={open}
        state={modalState}
        employee={employee}
        onCommit={onEmployeeUpdated}
        onCancel={() => setOpen(false)}
      />
      <Space size="middle" style={{ marginBottom: 16 }}>
        <Button onClick={onEmployeeAddRequested} type="primary">
          Add an employee
        </Button>
        <Search
          placeholder="Search by name, email or department"
          onSearch={onSearch}
          style={{ width: 400 }}
        />
      </Space>

      <Table dataSource={[...employees]} columns={columns}></Table>
    </div>
  );
};

export default EmployeeList;
