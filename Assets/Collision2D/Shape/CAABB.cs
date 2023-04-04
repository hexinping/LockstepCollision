using Lockstep.Math;
using Lockstep.UnsafeCollision2D;

namespace Lockstep.Collision2D {
    public class CAABB : CCircle {
        public override int TypeId => (int) EShape2D.AABB;
        public LVector2 size;

        public CAABB() : base(){ }

        public CAABB(LVector2 size){
            this.size = size; //宽 高  （x, z）
            radius = size.magnitude; //半径
        }
    }
}