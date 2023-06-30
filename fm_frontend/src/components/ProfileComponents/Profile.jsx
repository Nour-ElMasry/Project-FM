import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import GeneralAxiosService from "../../services/GeneralAxiosService";
import HomeTeam from "../HomeComponents/HomeTeam";
import Loading from "../Loading";
import ProfileCreateTeam from "./ProfileCreateTeam";
import ProfileHeader from "./ProfileHeader";
import decodeJwt from "jwt-decode";

const Profile = () => {
  const [user] = useState(JSON.parse(localStorage.getItem("User")));
  const isAdmin =
    decodeJwt(user.token)[
      "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
    ] === "Admin";

  const [loading, setLoading] = useState(!isAdmin);
  const navigate = useNavigate();
  const [team, setTeam] = useState({});

  const retire = () => {
    if (team !== null) {
      GeneralAxiosService.putMethod(
        "https://localhost:7067/api/v1/Users/" +
          user.customer.userId +
          "/Retire"
      ).then((res) => navigate("/home"));
    }
  };

  useEffect(() => {
    if (user == null) {
      navigate("");
    } else {
      if (!isAdmin) {
        GeneralAxiosService.getMethod(
          "https://localhost:7067/api/v1/Users/" +
            user.customer.userId +
            "/Team"
        )
          .then((res) => setTeam(res.data))
          .then(() => setLoading(false))
          .catch((err) => {
            setLoading(false);
          });
      }
    }
  }, []);

  return (
    <section className="profileSection container--pa">
      <h1 className="title">Profile</h1>
      {loading && <Loading />}
      {!loading && (
        <>
          <ProfileHeader
            hasTeam={user.customer.hasTeam}
            user={user.customer}
            retire={retire}
          />
          {!user.customer.hasTeam && !isAdmin && (
            <ProfileCreateTeam user={user} />
          )}
          {user.customer.hasTeam && !isAdmin && (
            <div
              className="homeSection"
              style={{ marginTop: "1rem", marginBottom: "0" }}
            >
              <div className="HomeComponents">
                <HomeTeam team={team} />
              </div>
            </div>
          )}
        </>
      )}
    </section>
  );
};
export default Profile;
