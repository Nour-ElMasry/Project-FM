import Button from "@mui/material/Button";
import { useState } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import CreateTeamForm from "./CreateTeamForm";

const CreateTeamLanding = (props) => {
  const [openForm, setOpenForm] = useState(false);

  const handleClickOpen = () => {
    setOpenForm(true);
  };

  const handleClose = () => {
    setOpenForm(false);
  };

  const handleTeamChoice = (data) => {
    GeneralAxiosService.postMethod(
      "https://localhost:7067/api/v1/Users/" +
        props.user.customer.userId +
        "/ChooseTeam",
      data
    ).then((res) => {
      props.user.customer.hasTeam = true;
      localStorage.setItem("User", JSON.stringify(props.user));
      window.location.reload();
    });
  };

  return (
    <>
      <CreateTeamForm
        open={openForm}
        handleClose={handleClose}
        handleTeamChoice={handleTeamChoice}
      />

      <div className="createTeamLanding container container--styled">
        <div className="createTeamLandingPic"></div>
        <div className="createTeamLandingInfo">
          <h3>Start your managing journey!</h3>
          <p>
            Choose your desired team and compete against opponents from the top
            leagues!
          </p>
          <Button
            onClick={handleClickOpen}
            disableElevation
            className="createBtn"
            color="success"
            variant="contained"
          >
            Choose Team
          </Button>
        </div>
      </div>
    </>
  );
};

export default CreateTeamLanding;
