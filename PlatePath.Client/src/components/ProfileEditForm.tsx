import React, { ChangeEvent, FormEvent, useState } from "react";
import {
  TextField,
  Button,
  Select,
  MenuItem,
  FormControl,
  InputLabel,
  SelectChangeEvent,
  Alert,
} from "@mui/material";
import { BoxContainer } from "./styled";
import { ProfileData } from "../pages/Profile/Profile";

const ProfileEditForm = ({
  setData,
}: {
  setData: (data: ProfileData) => void;
}) => {
  const [formData, setFormData] = useState({
    age: "",
    heightCm: "",
    weightKg: "",
    gender: "",
    activityLevel: "",
    weightGoal: "",
  });

  const handleChange = (
    e:
      | ChangeEvent<HTMLInputElement | { name?: string; value: unknown }>
      | SelectChangeEvent<string>
  ) => {
    if ("target" in e) {
      setFormData({
        ...formData,
        [e.target.name as string]: e.target.value as string,
      });
    } else {
      const selectEvent = e as SelectChangeEvent<string>;
      setFormData({
        ...formData,
        [selectEvent.target.name as string]: selectEvent.target.value,
      });
    }
  };

  let currentTimeout;
  const [showAlert, setShowAlert] = useState(false);

  const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    currentTimeout = null;
    setShowAlert(true);
    currentTimeout = setTimeout(() => {
      setShowAlert(false);
    }, 5000);
    console.log(formData);
    setData(formData as any);
  };

  return (
    <form onSubmit={handleSubmit} style={{ width: "100%" }}>
      {showAlert ? (
        <Alert sx={{ mb: "12px" }} severity="success">
          Plan submitted successfully
        </Alert>
      ) : null}
      <BoxContainer gap="25px" mb="12px" flexDirection="column" width="100%">
        <TextField
          label="Age"
          type="number"
          name="age"
          value={formData.age}
          onChange={handleChange}
          fullWidth
          required
        />
        <TextField
          label="Height (cm)"
          type="number"
          name="heightCm"
          value={formData.heightCm}
          onChange={handleChange}
          fullWidth
          required
        />
        <TextField
          label="Weight (kg)"
          type="number"
          name="weightKg"
          value={formData.weightKg}
          onChange={handleChange}
          fullWidth
          required
        />
        <FormControl fullWidth required>
          <InputLabel>Gender</InputLabel>
          <Select name="gender" value={formData.gender} onChange={handleChange}>
            <MenuItem value="Male">Male</MenuItem>
            <MenuItem value="Female">Female</MenuItem>
          </Select>
        </FormControl>
        <FormControl fullWidth required>
          <InputLabel>Activity Level</InputLabel>
          <Select
            name="activityLevel"
            value={formData.activityLevel}
            onChange={handleChange}
          >
            <MenuItem value="Sedentary">Sedentary</MenuItem>
            <MenuItem value="LightlyActive">Lightly active</MenuItem>
            <MenuItem value="ModeratelyActive">Moderately active</MenuItem>
            <MenuItem value="VeryActive">Very active</MenuItem>
            <MenuItem value="ExtraActive">Extra active</MenuItem>
          </Select>
        </FormControl>
        <FormControl fullWidth required>
          <InputLabel>Weight Goal</InputLabel>
          <Select
            name="weightGoal"
            value={formData.weightGoal}
            onChange={handleChange}
          >
            <MenuItem value="LooseWeight">Loose Weight</MenuItem>
            <MenuItem value="MaintainWeight">Maintain Weight</MenuItem>
            <MenuItem value="GainWeight">Gain Weight</MenuItem>
          </Select>
        </FormControl>
      </BoxContainer>
      <Button type="submit" variant="contained" color="primary" fullWidth>
        Save Changes
      </Button>
    </form>
  );
};

export default ProfileEditForm;
