using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // TODO: playerNumが 1 or 2以外取らないように保証する
    public int playerNum;
    public string aButton;
    public string horizontalButton;
    public string verticalButton;
    public Transform[] seedPrefabs;
    public int[] seedCounts;
    public int chosenOption = 0;
    public int score = 0;

    const float PLAYER_SQUARE_SIDE = 1.0f;
    const float PLAYER_SPEED = 10f;
    const float RAY_OFFSET_Y = 5f;
    const int COLLISION_LAYER = 1 << 10 | 1 << 11;
    const int SEED_LAYER = 1 << 9;
    const int RIGIDITY_TIME = 4;

    Vector3 prePos;
    Vector3 direction;

    Ray rayToForward;
    Ray rayToSeed;

    int enemyNum;
    int waitingCounter = RIGIDITY_TIME;


    void Start () {
        // playerの移動関連
        direction.Set(0, 0, 0);
        enemyNum = playerNum == 1 ? 2 : 1;
    }


    void Update() {
        // early returnできなかったのでやむなくフラグ管理
        bool skipFlag = false;

        if (PlayingStateController.PlayingState != PlayingStateEnum.playing) {
            skipFlag = true;
        }

        if (waitingCounter < RIGIDITY_TIME) {
            skipFlag = true;
        }

        if (!skipFlag && Input.GetButtonDown(aButton)) {
            PressedAButton();
            waitingCounter = 0;
        }

        if (!skipFlag && (Input.GetAxis(horizontalButton) != 0 || Input.GetAxis(verticalButton) != 0)) {
            PressedArrowButton();
            waitingCounter = 0;
        }

        waitingCounter++;
    }


    // 方向キーを押した時
    void PressedArrowButton() {
        direction.Set(Input.GetAxis(horizontalButton), 0, -Input.GetAxis(verticalButton));
        if (!WillCollision(direction)) {
            Vector3 targetPos = transform.position + direction * PLAYER_SQUARE_SIDE;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, PLAYER_SPEED);
        }
    }


    // 移動できるかの判定
    // 移動先に物体がある場合true(移動できない場合true)
    bool WillCollision(Vector3 direction) {
        rayToForward = new Ray(transform.position, direction);
        RaycastHit obj;
        if (Physics.Raycast(rayToForward, out obj, PLAYER_SQUARE_SIDE, COLLISION_LAYER)) {
            return true;
        } else {
            return false;
        }
    }


    // A Buttonを押した時
    void PressedAButton() {
        Vector3 rayPosition = transform.position;
        rayPosition.y += RAY_OFFSET_Y;
        rayToSeed = new Ray(rayPosition, -transform.up);
        RaycastHit seed;
        // rayがタネ系に当たった場合
        if(Physics.Raycast(rayToSeed, out seed, RAY_OFFSET_Y, SEED_LAYER)) {
            Transform seedTransform = seed.transform;
            // 足元に相手のタネ系オブジェクトがある
            if (seedTransform.name == enemyNum.ToString()) {
                return;
            }
            // 足元に自分の花オブジェクトがある
            else if (seedTransform.tag == "Flower") {
                TakeSeed(seedTransform);
            }
        }
        // 足元に何もオブジェクトがない
        else if(seedCounts[chosenOption] > 0) {
            SetSeed();
        }
    }


    // タネを植える
    void SetSeed() {
        Vector3 position = new Vector3(transform.position.x, 0f, transform.position.z);
        Transform seed = Instantiate(seedPrefabs[chosenOption], position, Quaternion.identity);
        seed.name = playerNum.ToString();
        seedCounts[chosenOption] -= 1;
    }


    // タネを取る(花を摘む)
    void TakeSeed(Transform seed) {
        seed.GetComponent<FlowerController>().TakeFlower(seedCounts);
        Destroy(seed.gameObject);
    }
}
