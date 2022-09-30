import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import TeamHeader from "./TeamHeader";
import TeamTabs from "./TeamTabs";
import TeamMatches from "./TeamMatches";
import TeamTabPanel from "./TeamTabPanel";
import Loading from "../Loading";
import TeamPlayers from "./TeamPlayers";

const Team = (props) => {
    const params = useParams();
    const [team, setTeam] = useState({});
    const [loading, setLoading] = useState(true);
    const [hasError, setHasError] = useState(false);
    const [user] = useState(JSON.parse(localStorage.getItem("User")));
    const navigate = useNavigate();

    const [value, setValue] = useState(0);

    const handleChange = (event, newValue) => {
      setValue(newValue);
    };

    useEffect(() => {
        if(user == null){
            navigate("/login");
        }else{
            GeneralAxiosService.getMethod("https://localhost:7067/api/v1/Teams/" + params.id)
            .then((response) => setTeam(response.data))
            .then(() => setLoading(false))
            .catch((e) => {
                setLoading(false);
                setHasError(true);
            });
        }
    }, [params, user, navigate])

    return <section className="teamSection container container--pa">
        <h1 className='title'>Team Details</h1>
        {hasError && <h2 className='errorMsg'>Oops! Something went wrong!</h2>}
        {loading && <Loading/>}

        {(!loading && !hasError) && <div className="teamHeader container container--styled">
            <TeamHeader teamName={team.name} ratings={team.currentTeamSheet} teamLogo={team.logo}/>
            <TeamTabs value={value} handleChange={handleChange}/>
        </div>
        }
        {(!loading && !hasError)  && <div className="tabContent">
            <TeamTabPanel value={value} index={0}>
                <TeamMatches teamId={team.id} played={false}/>
            </TeamTabPanel>
            <TeamTabPanel value={value} index={1}>
                <TeamMatches teamId={team.id} played={true}/>
            </TeamTabPanel>
            <TeamTabPanel value={value} index={2}>
                <TeamPlayers teamId={team.id}/>
            </TeamTabPanel>     
        </div>}
    </section>
}

export default Team;