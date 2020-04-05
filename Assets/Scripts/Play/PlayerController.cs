using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int playerNum;
    public string aButton;
    public string horizontalButton;
    public string verticalButton;
    float size = 1.0f;
    float speed = 10f;
    Vector3 target;
    Vector3 prePos;
    Vector3 movement;

    // Ray関連
    Ray rayToForward;
    int wallLayer = 1 << 10 | 1 << 11;
    Ray rayToSeed;
    int seedLayer = 1 << 9;

    // enemy関連
    int enemyNum;

    // タネ関連
    public Transform[] seedPrefabs;
    public int chosenOption = 0;
    public int[] seedCounts;

    // 得点
    public int score = 0;


    void Start () {
        // playerの移動関連
        target = transform.position;
        movement.Set(0, 0, 0);

        // enemyの番号
        enemyNum = CalcEnemyPlayerNumber(playerNum);
    }


    void Update()
    {
        if (PlayingStateController.PlayingState == PlayingStateEnum.playing) {
            // playerが移動済みかどうか
            if (transform.position == target) {
                if (Input.GetButtonDown(aButton)) {
                    GetSeedKey();
                } else {
                    SetTargetPosition();
                }
            }
        }
    }


    // 秒数固定の更新
    void FixedUpdate()
    {
        // playerを動かす
        Move();
    }


    // 方向キーを押した時
    void SetTargetPosition() {
        // TODO: 3回押されたら一気に動いてしまう？
        float h = Input.GetAxis(horizontalButton);
        float v = -Input.GetAxis(verticalButton);
        movement.Set(h, 0, v);
        // TODO: 壁の判定が甘い
        if (!RayToForwardd(movement)) {
            target = transform.position + movement * size;
        }
    }


    // playerを動かす
    void Move() {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }


    // 正面にレイを飛ばす
    bool RayToForwardd(Vector3 direction) {
        rayToForward = new Ray(transform.position, direction);
        RaycastHit obj;
        if (Physics.Raycast(rayToForward, out obj, 1.0f, wallLayer)) {
            return true;
        } else {
            return false;
        }
    }


    // return/spaceキーを押された時に呼ばれる関数
    void GetSeedKey() {
        Vector3 rayPosition = transform.position;
        rayPosition.y += 5f;
        rayToSeed = new Ray(rayPosition, -transform.up);
        RaycastHit seed;
        // rayがタネ系に当たった場合
        if(Physics.Raycast(rayToSeed, out seed, 5.0f, seedLayer)) {
            Transform seedTransform = seed.transform;
            // 足元に相手のタネ系オブジェクトがある
            if (seedTransform.name == enemyNum.ToString()) {
                return;
            }
            // 足元に自分の花オブジェクトがある
            else if (seedTransform.tag == "Flower") {
                TakeSeed(seedTransform); // 花を摘む
            }
        }
        // 足元に何もオブジェクトがない
        else if(seedCounts[chosenOption] > 0) {
            SetSeed();  // タネを植える
        }

    }


    // タネを植える
    void SetSeed() {
        string seedName = playerNum.ToString();
        Vector3 position = new Vector3(transform.position.x, 0f, transform.position.z);
        Transform seed = Instantiate(seedPrefabs[chosenOption], position, Quaternion.identity);
        seed.name = seedName;
        seedCounts[chosenOption] -= 1;
    }


    // タネを取る(花を摘む)
    void TakeSeed(Transform seed) {
        seed.GetComponent<FlowerController>().TakeFlower(seedCounts);
        Destroy(seed.gameObject);
    }


    // 相手のplayer番号を計算
    int CalcEnemyPlayerNumber(int n) {
        switch (n) {
            case 1:
                return 2;
            case 2:
                return 1;
            default:
                print("error with playerNum");
                break;
        }
        return 1;
    }
}
