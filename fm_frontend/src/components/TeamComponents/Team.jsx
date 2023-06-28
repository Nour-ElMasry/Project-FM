import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import TeamHeader from "./TeamHeader";
import HeaderTabs from "../TabComponents/HeaderTabs";
import TeamMatches from "./TeamMatches";
import TabPanel from "../TabComponents/TabPanel";
import Loading from "../Loading";
import TeamPlayers from "./TeamPlayers";
import ErrorMsg from "../ErrorMsg";
import TeamLineUp from "./TeamLineUp";

const Team = (props) => {
  const params = useParams();
  const [team, setTeam] = useState({});
  const [teamTactic, setTeamTactic] = useState("");
  const [loading, setLoading] = useState(true);
  const [hasError, setHasError] = useState(false);
  const [user] = useState(JSON.parse(localStorage.getItem("User")));
  const navigate = useNavigate();
  const [value, setValue] = useState(0);
  const [trigger, setTrigger] = useState(false);

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  const handleTeamTacticChange = async (tactic) => {
    await GeneralAxiosService.putMethod(
      "https://localhost:7067/api/v1/Users/" +
        user.customer.userId +
        "/Team/ChangeTactic",
      {
        newTactic: tactic,
      }
    ).then((res) => setTeamTactic(res.data));
  };

  useEffect(() => {
    if (user == null) {
      navigate("/");
    } else {
      GeneralAxiosService.getMethod(
        "https://localhost:7067/api/v1/Teams/" + params.id
      )
        .then((response) => {
          setTeam(response.data);
        })
        .then(() => setLoading(false))
        .catch((e) => {
          setLoading(false);
          setHasError(true);
        });
    }
  }, [params, user, navigate, teamTactic, trigger]);

  return (
    <section className="teamSection container container--pa">
      <h1 className="title">Team Details</h1>
      {hasError && <ErrorMsg />}
      {loading && <Loading />}

      {!loading && !hasError && (
        <div className="teamHeader container container--styled">
          <TeamHeader
            teamId={team.id}
            teamName={team.name}
            ratings={team.currentTeamSheet}
            teamLogo={team.logo}
            teamTactic={team.tactic}
            handleTeamTacticChange={handleTeamTacticChange}
          />
          <HeaderTabs
            tabs={["Matches", "Players", "Line-up"]}
            value={value}
            handleChange={handleChange}
          />
        </div>
      )}
      {!loading && !hasError && (
        <div className="tabContent">
          <TabPanel value={value} index={0}>
            <TeamMatches teamId={team.id} />
          </TabPanel>
          <TabPanel value={value} index={1}>
            <TeamPlayers teamId={team.id} />
          </TabPanel>
          <TabPanel value={value} index={2}>
            <TeamLineUp teamId={team.id} setTrigger={setTrigger} />
          </TabPanel>
        </div>
      )}
    </section>
  );
};

export default Team;
