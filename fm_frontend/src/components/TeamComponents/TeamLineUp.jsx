import { useEffect, useState } from "react";
import Loading from "../Loading";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import { MenuItem, TextField } from "@mui/material";
import Lineup from "./Lineup";

const TeamLineUp = (props) => {
  const [userTeamId] = useState(JSON.parse(localStorage.getItem("UserTeamId")));
  const [user] = useState(JSON.parse(localStorage.getItem("User")));

  const [lineup, setLineup] = useState({});
  const [formations, setFormations] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedFormation, setSelectedFormation] = useState(0);

  useEffect(() => {
    const fetchFormations = async () => {
      try {
        const res = await GeneralAxiosService.getMethod(
          `https://localhost:7067/api/v1/Teams/${props.teamId}/Formations`
        );
        setFormations(
          res.data.map((d, i) => {
            return {
              stringValue: [d.defenders, d.midfielders, d.attackers].join("-"),
              ...d,
              formationId: i,
            };
          })
        );
      } catch (e) {
        console.error(e);
      }
    };

    fetchFormations();
  }, []);

  useEffect(() => {
    const fetchLineup = async () => {
      const res = await GeneralAxiosService.getMethod(
        `https://localhost:7067/api/v1/Teams/${props.teamId}/LineUp`
      );
      let line = res.data;
      setLineup(line);
      let formation = [
        line.formation.defenders,
        line.formation.midfielders,
        line.formation.attackers,
      ].join("-");
      setSelectedFormation(
        formations.find((f) => f.stringValue === formation).formationId
      );
      setLoading(false);
    };

    if (formations.length > 0) {
      fetchLineup();
    }
  }, [formations, selectedFormation]);

  const handleOptionChange = async (event) => {
    setLoading(true);
    let formation = formations[event.target.value];
    await GeneralAxiosService.postMethod(
      `https://localhost:7067/api/v1/Users/${user.customer.userId}/Team/${userTeamId}/ChangeFormation`,
      {
        defenders: formation.defenders,
        midfielders: formation.midfielders,
        attackers: formation.attackers,
      }
    ).then((response) => {
      setSelectedFormation(event.target.value);
      props.setTrigger((prev) => !prev);
    });
  };

  return (
    <div>
      {loading && <Loading />}
      {!loading && (
        <div className="container--styled teamLineupContainer">
          <TextField
            required={props.required}
            select
            fullWidth
            autoComplete="off"
            value={selectedFormation}
            label="Team Formation"
            onChange={handleOptionChange}
            disabled={props.teamId !== userTeamId}
          >
            {formations.map((option) => (
              <MenuItem key={option.formationId} value={option.formationId}>
                {option.stringValue}
              </MenuItem>
            ))}
          </TextField>
          <Lineup startingEleven={lineup.startingEleven} bench={lineup.bench} />
        </div>
      )}
    </div>
  );
};

export default TeamLineUp;
