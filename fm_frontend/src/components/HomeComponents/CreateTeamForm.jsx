import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import DialogTitle from "@mui/material/DialogTitle";
import { Button, TextField, MenuItem } from "@mui/material";
import { useEffect, useState } from "react";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import Loading from "../Loading";
import Carousel from "./TeamsCarousel";

const CreateTeamForm = (props) => {
  const [selectedOption, setSelectedOption] = useState("");
  const [teams, setTeams] = useState("");

  const handleOptionChange = (event) => {
    setSelectedOption(event.target.value);
    setTeams(
      leagues.filter((l) => l.leagueId == event.target.value)[0].leagueTeams
    );
  };

  const [leagues, setLeagues] = useState([]);
  const [loading, setLoading] = useState(true);

  const [selectedTeam, setSelectedTeam] = useState({});

  useEffect(() => {
    GeneralAxiosService.getMethod(
      "https://localhost:7067/api/v1/Leagues/All/campain"
    )
      .then((response) => {
        setLeagues(
          response.data.map((l) => {
            return {
              leagueId: l.leagueId,
              leagueName: l.leagueName,
              leagueLogo: l.leagueLogo,
              leagueTeams: l.leagueTeams.sort((a, b) =>
                a.teamName.localeCompare(b.teamName)
              ),
            };
          })
        );
        setTeams(response.data[0].leagueTeams);
        setSelectedTeam({
          teamId: response.data[0].leagueTeams[0].teamId,
          teamName: response.data[0].leagueTeams[0].teamName,
        });
        setSelectedOption(response.data[0].leagueId);
      })
      .then(() => setLoading(false));
  }, []);

  const formHandleClose = () => {
    props.handleClose();
  };

  const onSubmitHandle = (e) => {
    e.preventDefault();
    formHandleClose();
    props.handleTeamChoice(selectedTeam);
  };

  return (
    <>
      <Dialog
        className="dialogContainer"
        open={props.open}
        onClose={formHandleClose}
        fullWidth
      >
        <DialogTitle>Choose Team</DialogTitle>
        <DialogContent>
          {loading ? (
            <Loading />
          ) : (
            <>
              <Box component="form" onSubmit={onSubmitHandle} sx={{ mt: 3 }}>
                <Grid container spacing={2}>
                  <Grid item xs={12} sm={12}>
                    <TextField
                      required={props.required}
                      select
                      fullWidth
                      autoComplete="off"
                      value={selectedOption}
                      label="Leagues"
                      onChange={handleOptionChange}
                    >
                      {leagues.map((option) => (
                        <MenuItem key={option.leagueId} value={option.leagueId}>
                          {option.leagueName}
                        </MenuItem>
                      ))}
                    </TextField>
                  </Grid>
                </Grid>
                <Carousel teams={teams} setSelectedTeam={setSelectedTeam} />
                <DialogActions sx={{ marginTop: "1.125rem" }}>
                  <Button onClick={formHandleClose}>Cancel</Button>
                  <Button type="submit">Confirm</Button>
                </DialogActions>
              </Box>
            </>
          )}
        </DialogContent>
      </Dialog>
    </>
  );
};

export default CreateTeamForm;
