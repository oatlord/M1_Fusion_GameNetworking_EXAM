// Commented out since this project uses Fusion 2, not PUN
// using System.Collections.Generic;
// using Photon.Pun;
// using Photon.Realtime;
// using TMPro;
// using UnityEngine;

// public class PhotonLobbyManager : MonoBehaviourPunCallbacks
// {
//     // ... (rest of the code)
// }
//                 }
//                 continue;
//             }

//             if (roomItems.ContainsKey(room.Name))
//             {
//                 roomItems[room.Name].GetComponent<RoomListItem>()
//                     .SetRoom(room.Name, room.PlayerCount, room.MaxPlayers);
//             }
//             else
//             {
//                 GameObject item = Instantiate(roomItemPrefab, roomListParent);
//                 RoomListItem listItem = item.GetComponent<RoomListItem>();
//                 listItem.SetRoom(room.Name, room.PlayerCount, room.MaxPlayers);
//                 listItem.lobbyManager = this;
//                 roomItems.Add(room.Name, item);
//             }
//         }
//     }

//     public void JoinListedRoom(string roomName)
//     {
//         PhotonNetwork.JoinRoom(roomName);
//         statusText.text = "Joining " + roomName + "...";
//     }
// }